using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Podio.API.Examples.MVC3.Controllers
{
    public class SamplesController : Controller
    {
        //
        // GET: /Samples/
        public ActionResult Index()
        {
            if (!Application.CurrentConnectionDetails.IsReadyToRubmble)
            {
                return RedirectToAction("Index", "Authorization");
            }

            if (Application.CurrentConnectionDetails.AuthorizationAccessToken.Ref.Type.Equals("app", StringComparison.CurrentCultureIgnoreCase))
            {
                return View("AppSamples", Application.CurrentConnectionDetails);
            }
            else
            {
                return View(Application.CurrentConnectionDetails);
            }
        }

    }
}
