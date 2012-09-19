using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Podio.API;
using Podio.API.Examples.MVC3.Models;

namespace Podio.API.Examples.MVC3
{
    public class Application
    {
        private const string CONNECTION_SESSION_KEY = "podio-connection-info";
        
        public static PodioConnectionInfo CurrentConnectionDetails
        { 
            get {
                if (HttpContext.Current.Session[CONNECTION_SESSION_KEY] != null)
                {
                    return HttpContext.Current.Session[CONNECTION_SESSION_KEY] as PodioConnectionInfo;
                }
                return new PodioConnectionInfo();
            }
            set {
                HttpContext.Current.Session[CONNECTION_SESSION_KEY] = value;
            }
        }
    }
}