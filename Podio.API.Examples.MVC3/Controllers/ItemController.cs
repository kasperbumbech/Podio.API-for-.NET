using Podio.API.Model;
using Podio.API.Services;
using Podio.API.Utils.ApplicationFields;
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
        public Client Client { get; set; }
        public Podio.API.Model.Application Application { get; set; }
        public Podio.API.Model.Item Item { get; set; }

        //
        // GET: /Item/Create
        [LoadClient(Order = 1), LoadApp(Order = 2)]
        public ActionResult Create()
        {
            return View(this.Application);
        }

        //
        // POST: /Item/Create

        [HttpPost, LoadClient(Order = 1), LoadApp(Order = 2)]
        public ActionResult Create(FormCollection collection)
        {
            var item = new Item();
            applyFieldValues(item, collection);

            this.Client.ItemService.AddNewItem((int)this.Application.AppId, item);
            return RedirectToRoute(new { controller = "Samples", action = "Index" });
        }

        //
        // GET: /Item/Update/1
        [LoadClient(Order = 1), LoadItem(Order = 2)]
        public ActionResult Update()
        {
            ViewData["app"] = this.Application;
            ViewData["item"] = this.Item;
            return View();
        }

        [HttpPut, LoadClient(Order = 1), LoadItem(Order = 2)]
        public ActionResult Update(FormCollection collection)
        {
            var item = new Item();
            item.ItemId = this.Item.ItemId;
            applyFieldValues(item, collection);
            this.Client.ItemService.UpdateItem(item);
            return RedirectToRoute(new { controller = "Samples", action = "Index" });
        }

        [HttpDelete, LoadClient]
        public ActionResult Delete(int itemId)
        {
            this.Client.ItemService.DeleteItem(itemId);
            return RedirectToRoute(new { controller = "Samples", action = "Index" });
        }

        private void applyFieldValues(Podio.API.Model.Item item, FormCollection collection)
        {
            foreach (var appField in Application.Fields)
            {
                var rawValue = collection[appField.ExternalId];
                if (!String.IsNullOrEmpty(rawValue) || appField.Type == "image")
                {
                    switch (appField.Type)
                    {
                        case "text":
                            var textField = item.Field<TextItemField>(appField.ExternalId);
                            textField.Value = rawValue;
                            break;
                        case "app":
                            var appRefField = item.Field<AppItemField>(appField.ExternalId);
                            appRefField.ItemIds = rawValue.Split(',').Select(id => int.Parse(id));
                            break;
                        case "contact":
                            var contactField = item.Field<ContactItemField>(appField.ExternalId);
                            contactField.ContactIds = rawValue.Split(',').Select(id => int.Parse(id));
                            break;
                        case "location":
                            var locationField = item.Field<LocationItemField>(appField.ExternalId);
                            locationField.Locations = new List<string>(rawValue.Split(','));
                            break;
                        case "duration":
                            var durationField = item.Field<DurationItemField>(appField.ExternalId);
                            durationField.Value = TimeSpan.FromSeconds(Int64.Parse(rawValue));
                            break;
                        case "progress":
                            var progressField = item.Field<ProgressItemField>(appField.ExternalId);
                            progressField.Value = int.Parse(rawValue);
                            break;
                        case "money":
                            var currency = collection[appField.ExternalId + "_currency"];
                            if (!String.IsNullOrEmpty(currency))
                            {
                                var moneyField = item.Field<MoneyItemField>(appField.ExternalId);
                                moneyField.ExternalId = appField.ExternalId;
                                moneyField.Value = int.Parse(rawValue);
                                moneyField.Currency = currency;
                            }
                            break;
                        case "date":
                            var dateField = item.Field<DateItemField>(appField.ExternalId);
                            dateField.Start = DateTime.Parse(rawValue);
                            var endString = collection[appField.ExternalId + "_end"];
                            if (!String.IsNullOrEmpty(endString))
                            {
                                dateField.End = DateTime.Parse(endString);
                            }
                            break;
                        case "question":
                        case "category":
                            var categoryAppField = Application.Field<CategoryApplicationField>(appField.ExternalId);
                            var categoryItemField = item.Field<CategoryItemField>(appField.ExternalId);
                            if (categoryAppField.Multiple)
                            {
                                categoryItemField.OptionIds = rawValue.Split(',').Select(id => int.Parse(id));
                            }
                            else
                            {
                                categoryItemField.OptionId = int.Parse(rawValue);
                            }
                            break;
                        case "embed":
                            var embedField = item.Field<EmbedItemField>(appField.ExternalId);
                            var embedUrls = new List<string>(rawValue.Split(','));
                            foreach (var embedUrl in embedUrls)
                            {
                                var embed = this.Client.EmbedService.AddAnEmbed(embedUrl.Trim());
                                if (embed.Files.Count > 0)
                                {
                                    embedField.AddEmbed((int)embed.EmbedId, embed.Files.First().FileId);
                                }
                                else
                                {
                                    embedField.AddEmbed((int)embed.EmbedId);
                                }
                            }
                            break;
                        case "image":
                            // This will break if app has more than one image field - each will get all the uploaded images
                            var fileIds = new List<int>();
                            foreach (string requestFile in Request.Files)
                            {
                                HttpPostedFileBase file = Request.Files[requestFile];
                                if (file.ContentLength > 0)
                                {
                                    byte[] data = new byte[file.ContentLength];
                                    file.InputStream.Read(data, 0, file.ContentLength);
                                    FileAttachment fileAttachment = this.Client.FileService.UploadFile(data, file.FileName, file.ContentType);
                                    fileIds.Add((int)fileAttachment.FileId);
                                }
                            }

                            if (fileIds.Count > 0)
                            {
                                var imageField = item.Field<ImageItemField>(appField.ExternalId);
                                imageField.FileIds = fileIds;
                            }
                            break;
                        case "state":
                            var stateItemField = item.Field<StateItemField>(appField.ExternalId);
                            stateItemField.Value = rawValue;
                            break;
                    }
                }
            }
        }

        private class LoadClientAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (!Podio.API.Examples.MVC3.Application.CurrentConnectionDetails.IsReadyToRubmble)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Authorization" }));
                }

                var client = Podio.API.Examples.MVC3.Application.CurrentConnectionDetails.GetClient();
                ((ItemController)filterContext.Controller).Client = client;
            }
        }

        private class LoadAppAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                Podio.API.Model.Application app = ((ItemController)filterContext.Controller).Client.ApplicationService.GetApp(Int32.Parse(filterContext.HttpContext.Request.QueryString["app_id"]));
                ((ItemController)filterContext.Controller).Application = app;
            }
        }

        private class LoadItemAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                Podio.API.Model.Item item = ((ItemController)filterContext.Controller).Client.ItemService.GetItem(Int32.Parse((string)filterContext.RouteData.Values["id"]));
                ((ItemController)filterContext.Controller).Item = item;

                Podio.API.Model.Application app = ((ItemController)filterContext.Controller).Client.ApplicationService.GetApp(Convert.ToInt32(item.App["app_id"]));
                ((ItemController)filterContext.Controller).Application = app;
            }
        }

    }
}
