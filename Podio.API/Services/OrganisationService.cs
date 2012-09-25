using Podio.API.Model;
using Podio.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;

namespace Podio.API.Services
{
    public class OrganisationService
    {
        private Client _client;

        public OrganisationService(Client client)
        {
            _client = client;
        }

        /// <summary>
        /// https://developers.podio.com/doc/organizations/get-organizations-22344
        /// </summary>
        public IEnumerable<Organization> GetOrganizations() {
            return PodioRestHelper.Request<List<Organization>>(Constants.PODIOAPI_BASEURL + "/org/", _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/organizations/get-organization-22383
        /// </summary>
        public Organization GetOrganization(int orgId) {
            return PodioRestHelper.Request<Organization>(Constants.PODIOAPI_BASEURL + "/org/" + orgId.ToString(), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/organizations/get-organization-by-url-22384
        /// </summary>
        public Organization GetOrganizationByURL(string url)
        {
            return PodioRestHelper.Request<Organization>(Constants.PODIOAPI_BASEURL + "/org/url", _client.AuthInfo.AccessToken, new Dictionary<string, string>() { { "url", url } }).Data;
        }
       
        /// <summary>
        /// Requires premium account
        /// https://developers.podio.com/doc/organizations/get-organization-member-50908
        /// </summary>
        public User GetOrganizationMember(int orgId,int userId) {
            // im not entirely sure that this is working as intended since this feature requires an upgraded account
            return PodioRestHelper.Request<User>(Constants.PODIOAPI_BASEURL + "/org/" + orgId + "/member/" + userId, _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// Requrires premium account
        /// https://developers.podio.com/doc/organizations/get-organization-members-50661
        /// </summary>
        public IEnumerable<User> GetOrganizationMembers(int orgId)
        {
            // im not entirely sure that this is working as intended since this feature requires an upgraded account
            return PodioRestHelper.Request<List<User>>(Constants.PODIOAPI_BASEURL + "/org/" + orgId + "/member/", _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// Im not sure this is implemented/or at least documented correct in the Podio end.
        /// https://developers.podio.com/doc/organizations/get-space-by-url-22388
        /// </summary>
        public Space GetSpaceByUrl(int orgId,string url) {
            return PodioRestHelper.Request<Space>(Constants.PODIOAPI_BASEURL + "/org/" + orgId + "/space/", _client.AuthInfo.AccessToken, new Dictionary<string, string> { { "url", url } }).Data;
        }


        /// <summary>
        /// https://developers.podio.com/doc/organizations/get-spaces-on-organization-22387
        /// </summary>
        public IEnumerable<Space> GetSpacesOnOrganization(int orgId)
        {
            return PodioRestHelper.Request<List<Space>>(Constants.PODIOAPI_BASEURL + "/org/" + orgId + "/space/", _client.AuthInfo.AccessToken).Data;
        }
 
        #region not implemented methods
        // Updateorganization
        // Addorganizationadmin
        // Createorganizationappstoreprofile
        // Deleteorganizationappstoreprofile
        // Deleteorganizationmemberrole
        // Endorganizationmembership
        // Getorganizationadmins
        // Getorganizationappstoreprofile
        // Getorganizationbillingprofile
        // Getorganizationloginreport
        // Getorganizationstatistics
        // Getsharedorganizations
        // Removeorganizationadmin
        // Updateorganizationappstoreprofile
        // Updateorganizationbillingprofile
        #endregion

     }
}
