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
        /// https://developers.podio.com/doc/applications/get-apps-by-space-22478
        /// </summary>
        public IEnumerable<Model.Application> GetAppsBySpace(int spaceId)
        {
            {
                return PodioRestHelper.Request<List<Model.Application>>(Constants.PODIOAPI_BASEURL + "/app/space/" + spaceId + "/", _client.AuthInfo.AccessToken).Data;
            }
        }

        public Model.Application GetApp(int appId)
        {
            return PodioRestHelper.Request<Model.Application>(Constants.PODIOAPI_BASEURL + "/app/" + appId, _client.AuthInfo.AccessToken).Data;
        }


        #region notimplemented
        //ActivateApp
        //AddNewApp
        //AddNewAppField
        //DeactivateApp
        //DeleteApp
        //DeleteAppField
        //GetAllUserApps
        //GetAppDependencies
        //GetAppField
        //GetAppOnSpaceByURLLabel
        //GetAppsAvailableForSpace
        //GetCalculationsForApp
        //GetFeatures
        //GetSpaceAppDependencies
        //GetTopApps
        //GetTopAppsForOrganization
        //InstallApp
        //UpdateAnAppField
        //UpdateApp
        //UpdateAppOrder
        #endregion

    }
}
