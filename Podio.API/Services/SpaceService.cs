using Podio.API.Model;
using Podio.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podio.API.Services
{
    public class SpaceService
    {
        private Client _client;

        public SpaceService(Client client) {
            _client = client;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/create-space-22390
        /// </summary>
        public Space.CreatedResponse CreateSpace(int orgId, string privacy, bool autoJoin, string name, bool postOnNewApp, bool postOnNewMember)
        {
            var requestData = new Space.CreateRequest() {
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
        public Space.CreatedResponse CreateSpace(Space.CreateRequest requestData)
        {
            return PodioRestHelper.JSONRequest<Space.CreatedResponse>(Constants.PODIOAPI_BASEURL + "/space/", _client.AuthInfo.access_token, requestData, PodioRestHelper.RequestMethod.POST).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/get-available-spaces-1911961
        /// </summary>
        public IEnumerable<Space> GetAvailableSpaces(int orgId) {
            return PodioRestHelper.Request<List<Space>>(Constants.PODIOAPI_BASEURL + "/space/org/" + orgId + "/available/", _client.AuthInfo.access_token).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/get-space-22389
        /// </summary>
        public Space GetSpace(int spaceId) {
            return PodioRestHelper.Request<Space>(Constants.PODIOAPI_BASEURL + "/space/" + spaceId, _client.AuthInfo.access_token).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/get-space-by-org-and-url-label-476929
        /// </summary>
        public Space GetSpaceByOrgAndURLLabel(int orgId,string urlLabel)
        {
            return PodioRestHelper.Request<Space>(Constants.PODIOAPI_BASEURL + "/space/org/" + orgId + "/" + urlLabel, _client.AuthInfo.access_token).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/get-space-by-url-22481
        /// </summary>
        public Space GetSpaceByUrl(string url)
        {
            return PodioRestHelper.Request<Space>(Constants.PODIOAPI_BASEURL + "/space/url", _client.AuthInfo.access_token, new Dictionary<string, string>() { { "url", url } }).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/get-top-spaces-22477
        /// </summary>
        public IEnumerable<Space> GetTopSpaces(int limit)
        {
            return PodioRestHelper.Request<List<Space>>(Constants.PODIOAPI_BASEURL + "/space/top/", _client.AuthInfo.access_token, new Dictionary<string, string>() { { "limit", limit.ToString() } }).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/update-space-22391
        /// </summary>
        public void UpdateSpace(int spaceId, Space.UpdateRequest requestData)
        {
           PodioRestHelper.PodioResponse t = PodioRestHelper.JSONRequest(Constants.PODIOAPI_BASEURL + "/space/" + spaceId, _client.AuthInfo.access_token, requestData, PodioRestHelper.RequestMethod.PUT);
           if (t.PodioError != null)
               throw new Exceptions.PodioResponseException(t.PodioError.error_description, t.PodioError);
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/create-space-22390
        /// </summary>
        public void UpdateSpace(int spaceId, string privacy, bool autoJoin, string name, bool postOnNewApp, bool postOnNewMember)
        {
            var requestData = new Space.UpdateRequest()
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
