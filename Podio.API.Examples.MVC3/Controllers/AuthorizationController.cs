using Podio.API.Examples.MVC3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Podio.API.Examples.MVC3.Controllers
{
    public class AuthorizationController : Controller
    {
        /// <summary>
        /// This view contains a nice form for inputting your 
        ///     1: client_id
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var conninfo = Application.CurrentConnectionDetails;
            if (conninfo.IsReadyToRubmble) // You sir - are ready to rumble
            {
                return View("ReadyToRumble", conninfo);
            }
            else if (conninfo.AuthorizationAccessToken == null && !string.IsNullOrEmpty(conninfo.ClientAppInfo.ClientSecret)) // You are now half-way - if we didn't loose you underways
            {
                return View("SetupConnection", conninfo); // You got a choice: App, User+Pass or Autorization for obtaining a token to go further with.
            }
            else // Lets get you ready to rumble
            {
                var viewmodel = new ClientAppInfo();
                viewmodel.Domain = Request.Url.GetLeftPart(UriPartial.Authority);
                return View(viewmodel);
            }
        }

        [HttpPost]
        public ActionResult Index(ClientAppInfo inf)
        {
            var conninfo = Application.CurrentConnectionDetails;
            conninfo.ClientAppInfo = inf;
            Application.CurrentConnectionDetails = conninfo;
            return RedirectToAction("index");
        }

        /// <summary>
        /// Setup a URL where Podio will redirect the browser to after Authorization (Podio adds a "?code=xxxxx" parameter to the url you specify)
        /// </summary>
        public void StartAuthorizeFlow() {
            string returnurl = Url.Encode(Url.Action("HandleAuthorizationResponse", "Authorization", new object { }, "http"));
            Response.Redirect("https://podio.com/oauth/authorize?client_id=" + Application.CurrentConnectionDetails.ClientAppInfo.ClientId + "&redirect_uri=" + returnurl);
        }

        /// <summary>
        /// Reset all auth info and go back to start.
        /// </summary>
        public ActionResult Reset() {
            Application.CurrentConnectionDetails = new PodioConnectionInfo();
            return RedirectToAction("index");
        }

        /// <summary>
        /// When you have authorized our application in Podio - you can use the code that podio returns 
        /// to authenticate against the podio API (hold on to it - it can be used again and again and again)....
        /// </summary>
        public ActionResult HandleAuthorizationResponse(string code, string error_reason, string error, string error_description)
        {
            // You got the code - congratulations - lets kick some $$S and connect to podio.
            if (string.IsNullOrEmpty(error) && !string.IsNullOrEmpty(code))
            {
                ClientAppInfo appinf = Application.CurrentConnectionDetails.ClientAppInfo;

                // We got a winner
                // Connect and fetch the authentication token for reuse.
                try
                {
                    var client = Podio.API.Client.ConnectWithAuthorizationCode(appinf.ClientId, appinf.ClientSecret, code, appinf.Domain);
                    var conninfo = Application.CurrentConnectionDetails;
                    conninfo.AuthorizationAccessToken = client.AuthInfo;
                    Application.CurrentConnectionDetails = conninfo;
                }
                catch(Exception ex) {
                    TempData["error"] = ex.Message;
                }
            }
            else
            {
                // Lets display an error
                TempData["error"] = error;
            }
            return RedirectToAction("Index");
        }

        public ActionResult ConnectApp(string appid, string apptoken)
        {
            ClientAppInfo appinf = Application.CurrentConnectionDetails.ClientAppInfo;
            try
            {
                var client = Podio.API.Client.ConnectAsApp(appinf.ClientId, appinf.ClientSecret,appid,apptoken,appinf.Domain);
                var conninfo = Application.CurrentConnectionDetails;
                conninfo.AuthorizationAccessToken = client.AuthInfo;
                Application.CurrentConnectionDetails = conninfo;
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public ActionResult ConnectUserAndPass(string username, string password)
        {
            ClientAppInfo appinf = Application.CurrentConnectionDetails.ClientAppInfo;
            try
            {
                var client = Podio.API.Client.ConnectAsUser(appinf.ClientId, appinf.ClientSecret,username,password);
                var conninfo = Application.CurrentConnectionDetails;
                conninfo.AuthorizationAccessToken = client.AuthInfo;
                Application.CurrentConnectionDetails = conninfo;
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
