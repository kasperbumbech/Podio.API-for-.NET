using Podio.API.Exceptions;
using Podio.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Podio.API
{
    /// <summary>
    /// Podio Hates .Net
    /// </summary>
    public class Client
    {
        // give a damn . this is immutable
        [DataContract]
        public struct AuthenticationResponse
        {
            [DataMember(IsRequired=false)]
            public string access_token { get; set; }
            [DataMember(IsRequired=false)]
            public string token_type { get; set; }
            [DataMember(IsRequired=false)]
            public int expires_in { get; set; }
            [DataMember(IsRequired = false)]
            public string refresh_token { get; set; }
        }

        /// <summary>
        /// Go use the static constructor
        /// </summary>
        private Client() { }

         // TODO: Implment refreshing the token if its invalid.
        public AuthenticationResponse AuthInfo { get; private set; }
        public string client_id { get; private set; }
        public string client_secret_ { get; private set; }

        /// <summary>
        /// Connecting as a User does not require that you have authorized the application.
        /// </summary>
        /// <param name="client_id">Your programs Id in podio</param>
        /// <param name="client_secret">The super secret and nasty guid Podio created</param>
        /// <param name="username">Your podio loginname (usually your email)</param>
        /// <param name="password">Podio enforces strong passwords of at least 8 characters length - old users can have shorter and unsafe passwords</param>
        /// <returns></returns>
        public static Client ConnectAsUser(string client_id, string client_secret, string username, string password)
        {
            var retval = new Client();
            // authenticate the username and password.
            string requestUri = Constants.PODIOAPI_BASEURL + "/oauth/token";

            Dictionary<string, string> _params = new Dictionary<string, string>() {
                { "grant_type","password"} ,
                {"username",username},
                {"password",password},
                {"client_id",client_id},
                {"client_secret",client_secret}
            };

            var _response = PodioRestHelper.Request<AuthenticationResponse>(requestUri, _params, PodioRestHelper.RequestMethod.POST);
            
            retval.AuthInfo = _response.Data;
            
            return retval;
        }


        /// <summary>
        /// Connecting as an app requires that you have authorized the app using the "Server-side flow" use the returned "code" that
        /// is the result of this interaction.
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Client ConnectAsApp(string client_id, string client_secret, string code)
        {
            // validate that the accessToken is valid.
            return new Client();
        }

        public Services.OrganisationService OrganisationService { get { return new Services.OrganisationService(this); } }

        public Services.SpaceService SpaceService { get { return new Services.SpaceService(this); } }


    }
}
