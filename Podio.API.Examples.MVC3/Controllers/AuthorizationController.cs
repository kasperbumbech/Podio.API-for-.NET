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
            return View();
        }

        [HttpPost]
        public void Index(string client_id)
        {
            // Setup a URL where Podio will redirect the browser to after Authorization (Podio adds a "?code=xxxxx" parameter to the url you specify)
            string returnurl = Url.Encode(Url.Action("HandleAuthorizationResponse", "Authorization", new object { },"http"));

            // We'll send the user to Podio for authorization.
            Response.Redirect("https://podio.com/oauth/authorize?client_id=" + client_id + "&redirect_uri=" + returnurl);
        }

        /// <summary>
        /// When you have authorized our application in Podio - you can use the code that podio returns 
        /// to authenticate against the podio API (hold on to it - it can be used again and again and again)....
        /// 
        /// You should use the Podio.API Client object to connect to the API
        /// </summary>
        /// <param name="code">The Authorization code - keep this to use the Podio REST API instead of User/Password</param>
        /// <returns></returns>
        public ActionResult HandleAuthorizationResponse(string code, string error_reason, string error, string error_description)
        {
            // You got the code - congratulations - lets kick some $$S and connect to podio.
            if (string.IsNullOrEmpty(error)  && !string.IsNullOrEmpty(code)) { 
                // We got a winner
               return RedirectToAction("AuthorizationSuccess",new {code = code});
            } 
            ViewData["error"] = error;
            return View();    
        }

        public ActionResult AuthorizationSuccess(string code) {
            ViewData["code"] = code;
            return View();
        }
    }
}
