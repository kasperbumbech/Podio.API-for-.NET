using Newtonsoft.Json;
using Podio.API.Exceptions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Podio.API.Utils
{

    /// <summary>
    ///  It smells like homebrew
    /// </summary>
    public sealed class PodioRestHelper
    {
        public enum RequestMethod
        {
            GET, POST, PUT, DELETE
        }

        public class PodioError
        {
            public bool error_propagate { get; set; }
            public string Error { get; set; }
            public string error_description { get; set; }
            public string error_detail { get; set; }
        }

        #region Dictionary converter
        private class NestedDictionaryConverter : Newtonsoft.Json.Converters.CustomCreationConverter<IDictionary<string, object>>
        {
            public override IDictionary<string, object> Create(Type objectType)
            {
                return new Dictionary<string, object>();
            }

            public override bool CanConvert(Type objectType)
            {
                // in addition to handling IDictionary<string, object>
                // we want to handle the deserialization of dict value
                // which is of type object
                return objectType == typeof(object) || base.CanConvert(objectType);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.StartObject
                    || reader.TokenType == JsonToken.Null)
                    return base.ReadJson(reader, objectType, existingValue, serializer);

                // if the next token is not an object
                // then fall back on standard deserializer (strings, numbers etc.)
                return serializer.Deserialize(reader);
            }
        }
        #endregion

        public class PodioResponse<T>
        {
            private PodioResponse _response;
            public PodioResponse(PodioResponse response)
            {
                _response = response;
            }
            public PodioResponse Response { get { return _response; } }
            public T Data
            {
                get
                {
                    if (_response.HttpStatusCode != HttpStatusCode.OK || _response.PodioError != null)
                    {
                        // if you been nasty and abused the API you will hit the rate_limit
                        if (_response.PodioError != null && _response.PodioError.error_description == "rate_limit")
                        {
                            throw new PodioRateLimitException();
                        }

                        throw new PodioResponseException(_response.PodioError.error_description, _response.PodioError);
                    }

                    return PodioRestHelper.Deserialize<T>(_response.Data);
                }
            }
        }

        public class PodioResponse
        {
            public string Data { get; set; }
            public HttpStatusCode HttpStatusCode { get; set; }
            public string ContentType { get; set; }
            public string RequestUri { get; set; }
            public Dictionary<string, string> RequestData { get; set; }
            public PodioError PodioError
            {
                get
                {
                    if (HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return null;
                    }
                    try
                    {
                        return PodioRestHelper.Deserialize<PodioError>(Data);
                    }
                    catch
                    {
                        //   throw new ArgumentException("Unable to deserialize JSON Response to podio error");
                    }
                    return null;
                }
            }
        }

        public static PodioResponse Request(string requestUri, Dictionary<string, string> requestData)
        {
            return Request(requestUri, null, requestData);
        }

        public static PodioResponse<T> Request<T>(string requestUri, Dictionary<string, string> requestData, RequestMethod requestMethod = RequestMethod.GET)
        {
            return new PodioResponse<T>(Request(requestUri, null, requestData, requestMethod));
        }

        public static PodioResponse Request(string requestUri, string accessToken, Dictionary<string, string> requestData = null, RequestMethod requestMethod = RequestMethod.GET)
        {
            requestData = requestData ?? new Dictionary<string, string>();

            // add the oauth token to the dictionary
            if (!string.IsNullOrEmpty(accessToken))
            {
                requestData.Add("oauth_token", accessToken);
            }

            HttpWebRequest request;

            if (requestMethod == RequestMethod.GET)
            {
                // lets add the different arguments if they are present
                if (requestData.Count > 0)
                {
                    requestUri = requestUri + "?" + string.Join("&", requestData.Select(x => x.Key + "=" + HttpUtility.UrlEncode(x.Value)).ToArray());
                }
                request = (HttpWebRequest)WebRequest.Create(requestUri);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "application/json";

            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(requestUri);

                request.Method = "POST";

                if (requestMethod == RequestMethod.PUT) request.Method = "PUT";


                string postData = string.Join("&", requestData.Select(x => x.Key != "" ? x.Key + "=" + x.Value : x.Value).ToArray());

                byte[] data = Encoding.UTF8.GetBytes(postData);

                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "application/json";
                request.ContentLength = data.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }
            }

            PodioResponse retval = GetResponse(request);

            retval.RequestData = requestData;
            retval.RequestUri = requestUri;

            return retval;
        }

        public static PodioResponse<T> Request<T>(string requestUri, string accessToken, Dictionary<string, string> requestData = null, RequestMethod requestMethod = RequestMethod.GET)
        {
            return new PodioResponse<T>(Request(requestUri, accessToken, requestData, requestMethod));
        }

        public static PodioResponse JSONRequest(string requestUri, string accessToken, object requestData, RequestMethod requestMethod)
        {
            if (requestMethod == RequestMethod.GET)
            {
                throw new ArgumentException("Only works with PUT/POST/DELETE");
            }
            requestUri = requestUri + "?oauth_token=" + accessToken;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);

            request.Method = "POST";

            if (requestMethod == RequestMethod.PUT) request.Method = "PUT";
            if (requestMethod == RequestMethod.DELETE) request.Method = "DELETE";

            string postData = Serialize(requestData);

            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "text/JSON";
            request.Accept = "application/json";
            request.ContentLength = data.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }
            PodioResponse retval = GetResponse(request);

            retval.RequestData = new Dictionary<string, string>() { { "json", postData } };
            retval.RequestUri = requestUri;

            return retval;
        }

        public static PodioResponse<T> JSONRequest<T>(string requestUri, string accessToken, object requestData, RequestMethod requestMethod)
        {

            PodioResponse<T> retval = new PodioResponse<T>(JSONRequest(requestUri, accessToken, requestData, requestMethod));
            return retval;
        }

        #region File upload
        public class FileParameter {
            public byte[] Data { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public FileParameter(byte[] data) : this(data, null) { }
            public FileParameter(byte[] data, string filename) : this(data, filename, null) { }
            public FileParameter(byte[] data, string filename, string contenttype) {
                Data = data;
                FileName = filename;
                ContentType = contenttype;
            }
        }

        public static PodioResponse<T> MultipartFormDataRequest<T>(string requestUri, string accessToken, Dictionary<string, object> requestData)
        {
            string boundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + boundary;

            MemoryStream ms = new MemoryStream();
            bool first = true;
            foreach (var key in requestData.Keys)
            {
                if (!first)
                    ms.Write(Encoding.UTF8.GetBytes("\r\n"), 0, Encoding.UTF8.GetByteCount("\r\n"));
                first = false;

                var value = requestData[key];
                if (value is FileParameter) {
                    FileParameter upload = (FileParameter)value;

                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        key,
                        upload.FileName ?? key,
                        upload.ContentType ?? "application/octet-stream");
                    ms.Write(Encoding.UTF8.GetBytes(header), 0, Encoding.UTF8.GetByteCount(header));

                    ms.Write(upload.Data, 0, upload.Data.Length);
                } else {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        key,
                        value);
                    ms.Write(Encoding.UTF8.GetBytes(postData), 0, Encoding.UTF8.GetByteCount(postData));
                }
            }
            string footer = "\r\n--" + boundary + "--\r\n";
            ms.Write(Encoding.UTF8.GetBytes(footer), 0, Encoding.UTF8.GetByteCount(footer));
            byte[] inputData = ms.ToArray(); ;

            requestUri = requestUri + "?oauth_token=" + accessToken;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);

            request.Method = "POST";
            request.ContentType = contentType;
            request.Accept = "application/json";
            request.ContentLength = inputData.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(inputData, 0, inputData.Length);
            }
            PodioResponse retval = GetResponse(request);
                
            retval.RequestUri = requestUri;
            PodioResponse<T> podioResponse = new PodioResponse<T>(retval);
            return podioResponse;
        }
        #endregion

        #region Privates and Helpers

        private static PodioResponse GetResponse(HttpWebRequest request)
        {
            //Console.WriteLine(String.Format("\n==> {0} {1} \n\n"));

            PodioResponse retval = new PodioResponse();
            try
            {
                using (var response = request.GetResponse())
                {
                    retval.ContentType = response.ContentType;
                    retval.HttpStatusCode = ((HttpWebResponse)response).StatusCode;

                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        retval.Data = sr.ReadToEnd();
                    }
                }
            }
            catch (WebException e) // we get an webexeption on everything but 200
            {
                using (WebResponse response = e.Response)
                {
                    retval.ContentType = response.ContentType;
                    retval.HttpStatusCode = ((HttpWebResponse)response).StatusCode;
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        retval.Data = sr.ReadToEnd();
                    }
                }
            }

            return retval;
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonConverter[] { new NestedDictionaryConverter() });

        }

        public static string Serialize(object t)
        {
            return JsonConvert.SerializeObject(t);
        }

        private static HttpWebRequest SetupPOSTRequest(string requestUri, Dictionary<string, string> requestData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "POST";
            string postData = string.Join("&", requestData.Select(x => x.Key + "=" + x.Value).ToArray());
            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "application/json";
            request.ContentLength = data.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }
            return request;

        }

        #endregion
    }
}

public static class ObjectExtensions
{
    public static T As<T>(this IDictionary<string, object> source)
        where T : class, new()
    {
        T someObject = new T();
        return (T)objectFromDict(someObject, source);
    }

    public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
    {
        return source.GetType().GetProperties(bindingAttr).ToDictionary
        (
            propInfo => propInfo.Name,
            propInfo => propInfo.GetValue(source, null)
        );
    }

    private static object objectFromDict(object someObject, IDictionary<string, object> source)
    {
        var propertyMap = new Dictionary<string, PropertyInfo>();
        foreach (var property in someObject.GetType().GetProperties())
        {
            var name = ((DataMemberAttribute[])property.GetCustomAttributes(typeof(DataMemberAttribute), false)).First().Name;
            propertyMap[name] = property;
        }
        //((DataMemberAttribute[])someObject.GetType().GetProperties().First().GetCustomAttributes(typeof(DataMemberAttribute), false)).First().Name

        foreach (KeyValuePair<string, object> item in source)
        {
            //someObject.GetType().GetProperty(item.Key).SetValue(someObject, item.Value, null);
            if (propertyMap.ContainsKey(item.Key))
            {
                var value = item.Value;
                if (value is Int64)
                {
                    // Convert 64 bit ints to 32 bit if required
                    value = Convert.ToInt32(value);
                }
                else if (value is Dictionary<string, object> && propertyMap[item.Key].PropertyType != typeof(Dictionary<string, object>))
                {
                    // Convert nested objects to appropriate type
                    var nestedObject = Activator.CreateInstance(propertyMap[item.Key].PropertyType);
                    value = objectFromDict(nestedObject, (Dictionary<string, object>)value);
                }
                else if (propertyMap[item.Key].PropertyType == typeof(DateTime?) && value != null)
                {
                    // Convert date strings to date times
                    value = DateTime.Parse((string)value);
                }
                else if (value is Newtonsoft.Json.Linq.JArray)
                {
                    var castedValue = (Newtonsoft.Json.Linq.JArray)value;
                    switch (propertyMap[item.Key].PropertyType.Name)
                    {
                        case "String[]":
                            value = castedValue.Select(s => s.ToString()).ToArray();
                            break;
                    }
                }
                propertyMap[item.Key].SetValue(someObject, value, null);
            }
        }
        return someObject;
    }
}