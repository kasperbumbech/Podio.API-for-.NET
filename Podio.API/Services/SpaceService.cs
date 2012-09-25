using Podio.API.Model;
using Podio.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace Podio.API.Services
{
    public class SpaceService
    {
        private Client _client;

        /// <summary>
        /// https://developers.podio.com/doc/spaces/create-space-22390
        /// </summary>
        [DataContract]
        public struct CreatedResponse
        {
            [DataMember(Name = "space_id")]
            public int SpaceId { get; set; }

            [DataMember(Name = "url")]
            public string Url { get; set; }
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/create-space-22390
        /// </summary>
        [DataContract]
        public struct CreateRequest
        {
            [DataMember(IsRequired = false, Name = "org_id")]
            public int OrgId { get; set; }

            [DataMember(IsRequired = false, Name = "privacy")]
            public string Privacy { get; set; }

            [DataMember(IsRequired = false, Name = "auto_join")]
            public bool AutoJoin { get; set; }

            [DataMember(IsRequired = false, Name = "name")]
            public string Name { get; set; }

            [DataMember(IsRequired = false, Name = "post_on_new_app")]
            public bool PostOnNewApp { get; set; }

            [DataMember(IsRequired = false, Name = "post_on_new_member")]
            public bool PostOnNewMember { get; set; }
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/update-space-22391
        /// </summary>
        [DataContract]
        public struct UpdateRequest
        {
            [DataMember(IsRequired = false, Name = "privacy")]
            public string Privacy { get; set; }

            [DataMember(IsRequired = false, Name = "auto_join")]
            public bool AutoJoin { get; set; }

            [DataMember(IsRequired = false, Name = "name")]
            public string Name { get; set; }

            [DataMember(IsRequired = false, Name = "post_on_new_app")]
            public bool PostOnNewApp { get; set; }

            [DataMember(IsRequired = false, Name = "post_on_new_member")]
            public bool PostOnNewMember { get; set; }
        }

        public SpaceService(Client client) {
            _client = client;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/create-space-22390
        /// </summary>
        public CreatedResponse CreateSpace(int orgId, string privacy, bool autoJoin, string name, bool postOnNewApp, bool postOnNewMember)
        {
            var requestData = new CreateRequest() {
                AutoJoin = autoJoin, 
                Name = name, 
                OrgId = orgId, 
                PostOnNewApp = postOnNewApp, 
                PostOnNewMember = postOnNewMember, 
                Privacy = privacy
            };
            return CreateSpace(requestData);
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/create-space-22390
        /// </summary>
        public CreatedResponse CreateSpace(CreateRequest requestData)
        {
            return PodioRestHelper.JSONRequest<CreatedResponse>(Constants.PODIOAPI_BASEURL + "/space/", _client.AuthInfo.AccessToken, requestData, PodioRestHelper.RequestMethod.POST).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/get-available-spaces-1911961
        /// </summary>
        public IEnumerable<Space> GetAvailableSpaces(int orgId) {
            return PodioRestHelper.Request<List<Space>>(Constants.PODIOAPI_BASEURL + "/space/org/" + orgId + "/available/", _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/get-space-22389
        /// </summary>
        public Space GetSpace(int spaceId) {
            return PodioRestHelper.Request<Space>(Constants.PODIOAPI_BASEURL + "/space/" + spaceId, _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/get-space-by-org-and-url-label-476929
        /// </summary>
        public Space GetSpaceByOrgAndURLLabel(int orgId,string urlLabel)
        {
            return PodioRestHelper.Request<Space>(Constants.PODIOAPI_BASEURL + "/space/org/" + orgId + "/" + urlLabel, _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/get-space-by-url-22481
        /// </summary>
        public Space GetSpaceByUrl(string url)
        {
            return PodioRestHelper.Request<Space>(Constants.PODIOAPI_BASEURL + "/space/url", _client.AuthInfo.AccessToken, new Dictionary<string, string>() { { "url", url } }).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/get-top-spaces-22477
        /// </summary>
        public IEnumerable<Space> GetTopSpaces(int limit)
        {
            return PodioRestHelper.Request<List<Space>>(Constants.PODIOAPI_BASEURL + "/space/top/", _client.AuthInfo.AccessToken, new Dictionary<string, string>() { { "limit", limit.ToString() } }).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/update-space-22391
        /// </summary>
        public void UpdateSpace(int spaceId, UpdateRequest requestData)
        {
           PodioRestHelper.PodioResponse t = PodioRestHelper.JSONRequest(Constants.PODIOAPI_BASEURL + "/space/" + spaceId, _client.AuthInfo.AccessToken, requestData, PodioRestHelper.RequestMethod.PUT);
           if (t.PodioError != null)
               throw new Exceptions.PodioResponseException(t.PodioError.error_description, t.PodioError);
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/create-space-22390
        /// </summary>
        public void UpdateSpace(int spaceId, string privacy, bool autoJoin, string name, bool postOnNewApp, bool postOnNewMember)
        {
            var requestData = new UpdateRequest()
            {
                AutoJoin = autoJoin,
                Name = name,
                PostOnNewApp = postOnNewApp,
                PostOnNewMember = postOnNewMember,
                Privacy = privacy
            };
            UpdateSpace(spaceId, requestData);
        }
    }
}
