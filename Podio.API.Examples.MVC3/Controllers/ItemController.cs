using Podio.API.Model;
using Podio.API.Utils.ItemFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Podio.API.Examples.MVC3.Controllers
{
    public class ItemController : Controller
    {
        public Podio.API.Model.Application Application { get; set; }

        //
        // GET: /Item/Create
        [LoadApp]
        public ActionResult Create()
        {
            return View(this.Application);
        }

        //
        // POST: /Item/Create

        [HttpPost, LoadApp]
        public ActionResult Create(FormCollection collection)
        {
            var item = new Item();

            //try
            //{
            foreach (var appField in Application.Fields)
            {
                switch (appField.Type) {
                    case "text":
                        var field = item.Field<TextItemField>(appField.ExternalId);
                        field.Value = collection[appField.ExternalId];
                        break;
                }
            }
                
                return RedirectToRoute(new { controller = "Samples", action = "Index" });
            //}
            //catch
            //{
            //    return View(this.Application);
            //}
        }

        private class LoadAppAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (!Podio.API.Examples.MVC3.Application.CurrentConnectionDetails.IsReadyToRubmble)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Authorization" }));
                }

                var client = Podio.API.Examples.MVC3.Application.CurrentConnectionDetails.GetClient();
                Podio.API.Model.Application app = client.ApplicationService.GetApp(Int32.Parse(filterContext.HttpContext.Request.QueryString["app_id"]));
                ((ItemController)filterContext.Controller).Application = app;
            }
        }

    }
}
