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
            [DataMember(IsRequired=false, Name="access_token")]
            public string AccessToken { get; set; }
            [DataMember(IsRequired=false,Name="token_type")]
            public string TokenType { get; set; }
            [DataMember(IsRequired=false,Name = "expires_in")]
            public int expires_in { get; set; }
            [DataMember(IsRequired = false,Name="refresh_token")]
            public string RefreshToken { get; set; }
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

            Dictionary<string, string> _requestbody = new Dictionary<string, string>() {
                { "grant_type","password"} ,
                {"username",username},
                {"password",password},
                {"client_id",client_id},
                {"client_secret",client_secret}
            };

            var _response = PodioRestHelper.Request<AuthenticationResponse>(requestUri, _requestbody, PodioRestHelper.RequestMethod.POST);
            
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
        public static Client ConnectAsApp(string client_id, string client_secret, string podioAppId, string podioAppToken, string redirectUri)
        {
            // validate that the accessToken is valid.
            //grant_type=app&app_id=YOUR_PODIO_APP_ID&app_token=YOUR_PODIO_APP_TOKEN&client_id=YOUR_APP_ID&redirect_uri=YOUR_URL&client_secret=YOUR_APP_SECRET
            var retval = new Client();
            // authenticate the username and password.
            string requestUri = Constants.PODIOAPI_BASEURL + "/oauth/token";

            Dictionary<string, string> _requestbody = new Dictionary<string, string>() {
                { "grant_type","app"} ,
                {"client_id",client_id},
                {"client_secret",client_secret},
                {"app_id",podioAppId},
                {"app_token",podioAppToken},
                {"redirect_uri",redirectUri}
            };
            var _response = PodioRestHelper.Request<AuthenticationResponse>(requestUri, _requestbody, PodioRestHelper.RequestMethod.POST);

            retval.AuthInfo = _response.Data;

            return retval;

        }

        /// <summary>
        /// Connecting with the Authorization code requires that you have authorized using the "Server-side flow" and use
        /// the returned "code" that is the result of the authorization.
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Client ConnectWithAuthorizationCode(string client_id, string client_secret, string authorizationCode, string redirectUri)
        {

            var retval = new Client();
            // authenticate the username and password.
            string requestUri = Constants.PODIOAPI_BASEURL + "/oauth/token";

            // grant_type=authorization_code&client_id=YOUR_APP_ID&redirect_uri=YOUR_URL&client_secret=YOUR_APP_SECRET&code=THE_AUTHORIZATION_CODE
            Dictionary<string, string> _requestbody = new Dictionary<string, string>() {
                { "grant_type","authorization_code"} ,
                {"client_id",client_id},
                {"client_secret",client_secret},
                {"code",authorizationCode},
                {"redirect_uri",redirectUri}
            };
            var _response = PodioRestHelper.Request<AuthenticationResponse>(requestUri, _requestbody, PodioRestHelper.RequestMethod.POST);

            retval.AuthInfo = _response.Data;

            return retval;
        }

        public Services.OrganisationService OrganisationService { get { return new Services.OrganisationService(this); } }

        public Services.SpaceService SpaceService { get { return new Services.SpaceService(this); } }

        public Services.ApplicationService ApplicationService { get { return new Services.ApplicationService(this); } }

        public Services.StreamService StreamService { get { return new Services.StreamService(this); } }

    }
}
