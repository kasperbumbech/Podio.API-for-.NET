using Podio.API.Model;
using Podio.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podio.API.Services
{
    public class ItemService
    {
        private Client _client;
        /// <summary>
        /// Add a client and you can use this as a shortcut to the Podio REST API 
        /// </summary>
        public ItemService(Client client)
        {
            _client = client;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-item-22360
        /// </summary>
        public Item GetItem(int itemId, bool markAsViewed = true)
        {
            Dictionary<string, string> args = new Dictionary<string, string>() { { "mark_as_viewed", markAsViewed.ToString() } };
            return PodioRestHelper.Request<Item>(Constants.PODIOAPI_BASEURL + "/item/" + itemId, _client.AuthInfo.AccessToken, args).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-items-27803
        /// </summary>
        public IEnumerable<Item> GetItems(int appId, int limit, int offset, string key = null, bool? remembered = null, string sortBy = null, bool? sortDesc = null, int? viewId = null) {
            Dictionary<string, string> args = new Dictionary<string, string>();

            args.Add("limit", limit.ToString());
            args.Add("offset", offset.ToString());

            if (!string.IsNullOrEmpty(key))
                args.Add("{key}", key);

            if (remembered != null)
                args.Add("remembered", remembered.ToString());

            if (!string.IsNullOrEmpty(sortBy))
                args.Add("sort_by", sortBy);
            if (sortDesc != null) 
                args.Add("sort_desc", sortDesc.ToString());
            if (viewId != null)
                args.Add("view_id", viewId.ToString());

            return PodioRestHelper.Request<List<Item>>(Constants.PODIOAPI_BASEURL + "/item/app/" + appId + "/", _client.AuthInfo.AccessToken,args).Data;
        }

        /*
         * 
AddNewItem
Calculate
DeleteItem
Delete item reference
Export items
Filter items
Filter items by view
Find items by field and title
Get app values
Get item basic
Get item field values
Get item preview for field reference
Get item references
Get item revision
Get item revision difference
Get item revisions
Get item values
Get items
Get items as Xlsx
Get meeting URL
Get references to item by field
Get top values for field
Revert item revision
Set participation
Update item
Update item field values
Update item reference
Update item values
         */
    }
}
