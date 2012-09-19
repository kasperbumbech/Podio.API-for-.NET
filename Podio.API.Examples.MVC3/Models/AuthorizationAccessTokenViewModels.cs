using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podio.API.Examples.MVC3.Models
{
    public class AuthorizationAccessTokenViewModel
    {
        public AuthorizationAccessTokenViewModel() {
            CurrentConnectionMethod = ConnectionMethod.NotConnected;
        }

        public enum ConnectionMethod {
            AsAPP, 
            AsUser, 
            WithAuthorization, 
            NotConnected
        }
        public ConnectionMethod CurrentConnectionMethod { get; set; }
        public Podio.API.Client.AuthorizationAccessToken Token { get; set; }

    }
}