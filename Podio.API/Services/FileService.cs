using Podio.API.Model;
using Podio.API.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Podio.API.Services
{
    public class FileService
    {
        private Client _client;
        /// <summary>
        /// Add a client and you can use this as a shortcut to the Podio REST API 
        /// </summary>
        public FileService(Client client)
        {
            _client = client;
        }

        /// <summary>
        /// https://developers.podio.com/doc/files/upload-file-1004361
        /// </summary>
        public FileAttachment UploadFile(byte[] data, string filename, string mimetype)
        {
            var requestData = new Dictionary<string, object> { 
                { "filename", filename }, 
                { "source", new Podio.API.Utils.PodioRestHelper.FileParameter(data, filename, mimetype) }
            };
            return PodioRestHelper.MultipartFormDataRequest<FileAttachment>(Constants.PODIOAPI_BASEURL + "/file/v2/", _client.AuthInfo.AccessToken, requestData).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/files/get-raw-file-1004147
        /// </summary>
        public byte[] GetRawFile(int fileId)
        {
            byte[] content;
            using (var request = new System.Net.WebClient())
            {
                string uri = String.Format("{0}/file/{1}/raw?oauth_token={2}", Constants.PODIOAPI_BASEURL, fileId, _client.AuthInfo.AccessToken);
                content = request.DownloadData(uri);
            }
           return content;
        }
    }
}
