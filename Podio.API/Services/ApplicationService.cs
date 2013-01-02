using Podio.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Podio.API.Services
{
    /// <summary>
    /// https://developers.podio.com/doc/applications
    /// </summary>
    public class ApplicationService
    {
        Client _client;
        internal ApplicationService(Client client)
        {
            _client = client;
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/activate-app-43822
        /// </summary>
        public PodioRestHelper.PodioResponse ActivateApp(int appId)
        {
            return PodioRestHelper.JSONRequest(String.Format("{0}/app/{1}/activate", Constants.PODIOAPI_BASEURL, appId), _client.AuthInfo.AccessToken, null, PodioRestHelper.RequestMethod.POST);
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/deactivate-app-43821
        /// </summary>
        public PodioRestHelper.PodioResponse DeactivateApp(int appId)
        {
            return PodioRestHelper.JSONRequest(String.Format("{0}/app/{1}/deactivate", Constants.PODIOAPI_BASEURL, appId), _client.AuthInfo.AccessToken, null, PodioRestHelper.RequestMethod.POST);
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/delete-app-43693
        /// </summary>
        public PodioRestHelper.PodioResponse DeleteApp(int appId)
        {
            return PodioRestHelper.JSONRequest(String.Format("{0}/app/{1}", Constants.PODIOAPI_BASEURL, appId), _client.AuthInfo.AccessToken, null, PodioRestHelper.RequestMethod.DELETE);
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/delete-app-field-22355
        /// </summary>
        public PodioRestHelper.PodioResponse DeleteAppField(int appId, int fieldId, bool? deleteValues = null)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            if (deleteValues != null) { args.Add("delete_values", deleteValues.ToString()); }
            return PodioRestHelper.JSONRequest(String.Format("{0}/app/{1}/field/{2}", Constants.PODIOAPI_BASEURL, appId, fieldId), _client.AuthInfo.AccessToken, args, PodioRestHelper.RequestMethod.DELETE);
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/get-apps-by-space-22478
        /// </summary>
        public IEnumerable<Model.Application> GetAppsBySpace(int spaceId)
        {
            return PodioRestHelper.Request<List<Model.Application>>(String.Format("{0}/app/space/{1}/", Constants.PODIOAPI_BASEURL, spaceId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/get-app-22349
        /// </summary>
        public Model.Application GetApp(int appId)
        {
            return PodioRestHelper.Request<Model.Application>(String.Format("{0}/app/{1}", Constants.PODIOAPI_BASEURL, appId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/get-all-user-apps-5902728
        /// </summary>
        public IEnumerable<Model.Application> GetUserApps()
        {
            return PodioRestHelper.Request<List<Model.Application>>(String.Format("{0}/app/v2/", Constants.PODIOAPI_BASEURL), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/get-app-dependencies-39159
        /// </summary>
        public IEnumerable<Model.Application> GetAppDependencies(int appId)
        {
            return PodioRestHelper.Request<List<Model.Application>>(String.Format("{0}/app/{1}/dependencies/", Constants.PODIOAPI_BASEURL, appId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/get-app-field-22353
        /// </summary>
        public Model.ApplicationField GetAppField(int appId, int fieldId)
        {
            return PodioRestHelper.Request<Model.ApplicationField>(String.Format("{0}/app/{1}/field/{2}", Constants.PODIOAPI_BASEURL, appId, fieldId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/get-apps-available-for-space-29761
        /// </summary>
        public IEnumerable<Model.Application> GetAvailableAppsForSpace(int spaceId)
        {
            return PodioRestHelper.Request<List<Model.Application>>(String.Format("{0}/app/space/{1}/available/", Constants.PODIOAPI_BASEURL, spaceId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/get-top-apps-22476
        /// </summary>
        public IEnumerable<Model.Application> GetTopApps(int? limit = null)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            if (limit == null) { args.Add("limit", limit.ToString()); }
            return PodioRestHelper.Request<List<Model.Application>>(String.Format("{0}/app/top/", Constants.PODIOAPI_BASEURL), _client.AuthInfo.AccessToken,args).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/get-top-apps-for-organization-1671395
        /// </summary>
        public IEnumerable<Model.Application> GetTopAppsForOrganization(int orgId)
        {
            return PodioRestHelper.Request<List<Model.Application>>(String.Format("{0}/app/org/{1}/top/", Constants.PODIOAPI_BASEURL,orgId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/install-app-22506
        /// </summary>
        public PodioRestHelper.PodioResponse InstallApp(int appId, int spaceId)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("space_id", spaceId.ToString());
            return PodioRestHelper.JSONRequest(String.Format("{0}/app/{1}/install", Constants.PODIOAPI_BASEURL, appId), _client.AuthInfo.AccessToken, args, PodioRestHelper.RequestMethod.POST);
        }

        /// <summary>
        /// https://developers.podio.com/doc/applications/update-app-order-22463
        /// </summary>
        public PodioRestHelper.PodioResponse UpdateAppOrder(int spaceId, IEnumerable<int> appIds)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("app_ids", String.Join(",", appIds.Select(id => id.ToString()).ToArray()));
            return PodioRestHelper.JSONRequest(String.Format("{0}/app/space/{1}/order", Constants.PODIOAPI_BASEURL, spaceId), _client.AuthInfo.AccessToken, args, PodioRestHelper.RequestMethod.POST);
        }
        #region notimplemented

        //AddNewApp
        //AddNewAppField
        //GetAppOnSpaceByURLLabel
        //GetCalculationsForApp
        //GetFeatures
        //GetSpaceAppDependencies
        //UpdateAnAppField
        //UpdateApp
        #endregion

    }
}
