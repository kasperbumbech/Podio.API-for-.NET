using System.Web.Configuration;
using Podio.API.Exceptions;
using Podio.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace Podio.API
{
    public class Client
    {

        // This is what you want to hold on to if you
        // are to keep an open connection to Podio.
        [DataContract]
        public class AuthorizationAccessToken
        {
            [DataMember(IsRequired = false, Name = "access_token")]
            public string AccessToken { get; set; }

            [DataMember(IsRequired = false, Name = "token_type")]
            public string TokenType { get; set; }

            [DataMember(IsRequired = false, Name = "expires_in")]
            public int ExpiresIn { get; set; }

            [DataMember(IsRequired = false, Name = "refresh_token")]
            public string RefreshToken { get; set; }

            [DataMember(IsRequired = false, Name = "time_obtained")]
            public DateTime TimeObtained { get; set; }

            [DataMember(IsRequired = false, Name = "ref")]
            public Model.Ref Ref { get; set; }
        }

        /// <summary>
        /// Go use the static constructor
        /// </summary>
        
        public Client(AuthorizationAccessToken authInfo, string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this._authinfo = authInfo;
        }

        private AuthorizationAccessToken _authinfo;

        /// <summary>
        /// This automagically refreshes the token if its getting near an invalidated 
        /// state.
        /// </summary>
        public AuthorizationAccessToken AuthInfo
        {
            get
            {
                ValidateConnection();
                return _authinfo;
            }
            private set
            {
                _authinfo = value;
            }
        }

        private string clientId { get; set; }
        private string clientSecret { get;set; }

        private void ValidateConnection()
        {
            if (this._authinfo.TimeObtained.AddSeconds(this._authinfo.ExpiresIn) < DateTime.Now.AddSeconds(-10))
            {
                string requestUri = Constants.PODIOAPI_BASEURL + "/oauth/token";
                Dictionary<string, string> _requestbody = new Dictionary<string, string>() {
                    {"grant_type","refresh_token"},
                    {"client_id",clientId},
                    {"client_secret",clientSecret},
                    {"refresh_token",_authinfo.RefreshToken}
                };

                this._authinfo = PodioRestHelper.Request<AuthorizationAccessToken>(requestUri, _requestbody, PodioRestHelper.RequestMethod.POST).Data;
                this._authinfo.TimeObtained = DateTime.Now; // mark the date and time obtained
            }
        }

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
            // authenticate the username and password.
            string requestUri = Constants.PODIOAPI_BASEURL + "/oauth/token";

            Dictionary<string, string> _requestbody = new Dictionary<string, string>() {
                { "grant_type","password"} ,
                {"username",username},
                {"password",password},
                {"client_id",client_id},
                {"client_secret",client_secret}
            };

            var _response = PodioRestHelper.Request<AuthorizationAccessToken>(requestUri, _requestbody, PodioRestHelper.RequestMethod.POST).Data;
            _response.TimeObtained = DateTime.Now;

            return new Client(_response, client_id, client_secret);
        }
        
        /// <summary>
        /// Connecting as an app requires that you have authorized the app using the "Server-side flow" use the returned "code" that
        /// is the result of this interaction.
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Client ConnectAsApp(string clientId, string clientSecret, string podioAppId, string podioAppToken, string redirectUri)
        {
            // validate that the accessToken is valid.
            //grant_type=app&app_id=YOUR_PODIO_APP_ID&app_token=YOUR_PODIO_APP_TOKEN&client_id=YOUR_APP_ID&redirect_uri=YOUR_URL&client_secret=YOUR_APP_SECRET

            // authenticate the username and password.
            string requestUri = Constants.PODIOAPI_BASEURL + "/oauth/token";

            Dictionary<string, string> _requestbody = new Dictionary<string, string>() {
                { "grant_type","app"} ,
                {"client_id",clientId},
                {"client_secret",clientSecret},
                {"app_id",podioAppId},
                {"app_token",podioAppToken},
                {"redirect_uri",redirectUri}
            };
            var _response = PodioRestHelper.Request<AuthorizationAccessToken>(requestUri, _requestbody, PodioRestHelper.RequestMethod.POST).Data;
            _response.TimeObtained = DateTime.Now;

            return new Client(_response, clientId, clientSecret);
        }

        /// <summary>
        /// Connecting with the Authorization code requires that you have authorized using the "Server-side flow" and use
        /// the returned "code" that is the result of the authorization.
        /// 
        /// Rememeber that this is a one-time enjoyment and that you should hold on to the clients AuthorizationAccessToken if you want to 
        /// keep your APP's ability to be re-authorized.
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Client ConnectWithAuthorizationCode(string clientId, string clientSecret, string authorizationCode, string redirectUri)
        {
            // authenticate the username and password.
            string requestUri = Constants.PODIOAPI_BASEURL + "/oauth/token";

            // grant_type=authorization_code&client_id=YOUR_APP_ID&redirect_uri=YOUR_URL&client_secret=YOUR_APP_SECRET&code=THE_AUTHORIZATION_CODE
            Dictionary<string, string> _requestbody = new Dictionary<string, string>() {
                { "grant_type","authorization_code"} ,
                {"client_id",clientId},
                {"client_secret",clientSecret},
                {"code",authorizationCode},
                {"redirect_uri",redirectUri}
            };

            var _response = PodioRestHelper.Request<AuthorizationAccessToken>(requestUri, _requestbody, PodioRestHelper.RequestMethod.POST).Data;
            _response.TimeObtained = DateTime.Now;

            return new Client(_response, clientId, clientSecret);
        }

        public Services.OrganisationService OrganisationService { get { return new Services.OrganisationService(this); } }

        public Services.SpaceService SpaceService { get { return new Services.SpaceService(this); } }

        public Services.ApplicationService ApplicationService { get { return new Services.ApplicationService(this); } }

        public Services.StreamService StreamService { get { return new Services.StreamService(this); } }

        public Services.ItemService ItemService { get { return new Services.ItemService(this); } }

        public Services.EmbedService EmbedService { get { return new Services.EmbedService(this); } }

        public Services.FileService FileService { get { return new Services.FileService(this); } }

        public Services.TaskService TaskService { get { return new Services.TaskService(this); } }

        public Services.ContactService ContactService { get { return new Services.ContactService(this); } }
    }
}
