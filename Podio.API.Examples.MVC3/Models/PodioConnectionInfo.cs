using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Podio.API.Examples.MVC3.Models
{
    public class ClientAppInfo {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string Domain { get; set; }
    }

    public class PodioConnectionInfo
    {
        public PodioConnectionInfo() {
            ClientAppInfo = new ClientAppInfo();
        }
        
        public Podio.API.Client.AuthorizationAccessToken AuthorizationAccessToken { get; set; }
        public ClientAppInfo ClientAppInfo { get; set; }

        public bool IsReadyToRubmble {
            get {
                if (AuthorizationAccessToken == null || string.IsNullOrEmpty(ClientAppInfo.ClientId) || string.IsNullOrEmpty(ClientAppInfo.ClientSecret) || string.IsNullOrEmpty(ClientAppInfo.ClientSecret))
                {
                    return false;
                }
                return true;
            }
        }

        private Client _client = null;
        public Client GetClient()
        {
            if (!IsReadyToRubmble)
            {
                throw new ArgumentException("Go setup clientid, clientsecret and authorize/authenticate asking for a client");
            }
            if (_client == null)
            {
                _client = new Client(AuthorizationAccessToken, ClientAppInfo.ClientId, ClientAppInfo.ClientSecret);
            }
            return _client;
        }
    }
}