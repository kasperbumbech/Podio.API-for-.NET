using Podio.API.Model;
using Podio.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Podio.API.Services
{
    public class EmbedService
    {
        private Client _client;
        /// <summary>
        /// Add a client and you can use this as a shortcut to the Podio REST API 
        /// </summary>
        public EmbedService(Client client)
        {
            _client = client;
        }

        [DataContract]
        public struct CreateRequest
        {
            [DataMember(IsRequired = false, Name = "url")]
            public string Url { get; set; }
        }

        /// <summary>
        /// https://developers.podio.com/doc/embeds/add-an-embed-726483
        /// </summary>
        public Embed AddAnEmbed(string url)
        {
            var requestData = new CreateRequest()
            {
                Url = url
            };
            return PodioRestHelper.JSONRequest<Embed>(Constants.PODIOAPI_BASEURL + "/embed/", _client.AuthInfo.AccessToken, requestData, PodioRestHelper.RequestMethod.POST).Data;
        }

    }
}
