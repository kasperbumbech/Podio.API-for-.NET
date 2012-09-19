using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podio.API.Examples.MVC3.Models
{
    public class PodioConnectionInfo
    {
        public Podio.API.Client.AuthorizationAccessToken AuthorizationAccessToken { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public bool IsReadyToRubmble {
            get {
                if (AuthorizationAccessToken == null || !string.IsNullOrEmpty(ClientId) || !string.IsNullOrEmpty(ClientSecret))
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
                _client = new Client(AuthorizationAccessToken, ClientId, ClientSecret);
            }
            return _client;
        }
    }
}