using Podio.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IEnumerable<Model.App> GetAppsBySpace(int spaceId)
        {
            {
                return PodioRestHelper.Request<List<Model.App>>(Constants.PODIOAPI_BASEURL + "/app/space/" + spaceId + "/", _client.AuthInfo.access_token).Data;
            }
        }




        #region notimplemented
        //ActivateApp
        //AddNewApp
        //AddNewAppField
        //DeactivateApp
        //DeleteApp
        //DeleteAppField
        //GetAllUserApps
        //GetApp
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
