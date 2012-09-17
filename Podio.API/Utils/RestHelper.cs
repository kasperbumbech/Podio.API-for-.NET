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
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Podio.API.Utils
{
  
    [Serializable]
    public class SerializableDictionary : ISerializable
    {
        public Dictionary<string, object[]> dict;
        public SerializableDictionary()
        {
            dict = new Dictionary<string, object[]>();
        }


        protected SerializableDictionary(SerializationInfo info, StreamingContext context)
        {
            dict = new Dictionary<string, object[]>();
            foreach (var entry in info)
            {
                object[] array = entry.Value as object[];
                dict.Add(entry.Name, array);
            }
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            foreach (string key in dict.Keys)
            {
                info.AddValue(key, dict[key]);
            }
        }
    }


   /// <summary>
   ///  It smells like homebrew
   /// </summary>
    public sealed class PodioRestHelper
    {
        public class PodioError
        {
            public bool error_propagate { get; set; }
            public string Error { get; set; }
            public string error_description { get; set; }
            public string error_detail { get; set; }
        }

        public enum RequestMethod
        {
            GET, POST, PUT
        }

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
                        if (_response.PodioError != null && _response.PodioError.error_description == "rate_limit") {
                            throw new PodioRateLimitException();
                        }
                        
                        throw new PodioResponseException(_response.PodioError.error_description, _response.PodioError);
                    }

                    return PodioRestHelper.Deserialise<T>(_response.Data);
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
                        return PodioRestHelper.Deserialise<PodioError>(Data);
                    }
                    catch
                    {
                        //   throw new ArgumentException("Unable to deserialize JSON Response to podio error");
                    }
                    return null;
                }
            }
        }

        public static PodioResponse Request(string requestUri, Dictionary<string, string> requestData) {
            return Request(requestUri,null,requestData);
        }

        public static PodioResponse<T> Request<T>(string requestUri, Dictionary<string, string> requestData, RequestMethod requestMethod = RequestMethod.GET)
        {
            return new PodioResponse<T>(Request(requestUri, null, requestData,requestMethod));
        }
     
        public static PodioResponse Request(string requestUri, string accessToken, Dictionary<string, string> requestData = null, RequestMethod requestMethod = RequestMethod.GET) {
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
                    requestUri = requestUri + "?" + string.Join("&", requestData.Select(x => x.Key + "=" + HttpUtility.UrlEncode(x.Value)));
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
                    

                string postData = string.Join("&", requestData.Select(x => x.Key != "" ? x.Key + "=" + x.Value : x.Value));

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

        public static PodioResponse JSONRequest(string requestUri, string accessToken, object requestData, RequestMethod requestMethod) {
            if (requestMethod == RequestMethod.GET) {
                throw new ArgumentException("Only works with PUT/POST");
            }
            requestUri = requestUri + "?oauth_token=" + accessToken;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            
            request.Method = "POST";
            
            if (requestMethod == RequestMethod.PUT) request.Method = "PUT";
                   
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

        #region Privates and Helpers

        private static PodioResponse GetResponse(HttpWebRequest request)
        {
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

        private static T Deserialise<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                obj = (T)serializer.ReadObject(ms);
                return obj;
            }
        }

        private static string Serialize(object t)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(t.GetType());
            string retval;
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, t);
                 retval = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
            }
            return retval;
        }  

        private static HttpWebRequest SetupPOSTRequest(string requestUri, Dictionary<string, string> requestData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "POST";
            string postData = string.Join("&", requestData.Select(x => x.Key + "=" + x.Value));
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
