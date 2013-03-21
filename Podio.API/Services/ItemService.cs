using Podio.API.Model;
using Podio.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


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
        public PodioCollection<Item> GetItems(int appId, int limit, int offset, KeyValuePair<string, string>? key = null, bool? remembered = null, string sortBy = null, bool? sortDesc = null, int? viewId = null)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();

            args.Add("limit", limit.ToString());
            args.Add("offset", offset.ToString());

            if (key.HasValue)
                args.Add(key.Value.Key, key.Value.Value);

            if (remembered != null)
                args.Add("remembered", remembered.ToString());

            if (!string.IsNullOrEmpty(sortBy))
                args.Add("sort_by", sortBy);
            if (sortDesc != null) 
                args.Add("sort_desc", sortDesc.ToString());
            if (viewId != null)
                args.Add("view_id", viewId.ToString());

            return PodioRestHelper.Request<PodioCollection<Item>>(Constants.PODIOAPI_BASEURL + "/item/app/" + appId + "/", _client.AuthInfo.AccessToken,args).Data;
        }

        [DataContract]
        public struct FilterRequest
        {
            [DataMember(IsRequired = false, Name = "filters")]
            //public IDictionary<string, string> Filters { get; set; }  // Changed to below to fix FilterItem for category/question field type
            public IDictionary<string, object> Filters { get; set; }

            [DataMember(IsRequired = false, Name = "sort_by")]
            public string SortBy { get; set; }

            [DataMember(IsRequired = false, Name = "sort_desc")]
            public bool? SortDesc { get; set; }

            [DataMember(IsRequired = false, Name = "limit")]
            public int? Limit { get; set; }

            [DataMember(IsRequired = false, Name = "offset")]
            public int? Offset { get; set; }

            [DataMember(IsRequired = false, Name = "remember")]
            public bool? Remember { get; set; }
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/filter-items-4496747
        /// </summary>
        //public PodioCollection<Item> FilterItems(int appId, int limit, int offset, Dictionary<string, string> filters = null, bool? remembered = null, string sortBy = null, bool? sortDesc = null)
        // Changed to below to fix FilterItem for category/question field type
        public PodioCollection<Item> FilterItems(int appId, int limit, int offset, Dictionary<string, object> filters = null, bool? remembered = null, string sortBy = null, bool? sortDesc = null)
        {
            var requestData = new FilterRequest()
            {
                Filters = filters,
                SortBy = sortBy,
                SortDesc = sortDesc,
                Limit = limit,
                Offset = offset,
                Remember = remembered
            };
            return FilterItems(appId, requestData);
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/add-new-item-22362
        /// </summary>
        public PodioCollection<Item> FilterItems(int appId, FilterRequest requestData)
        {
            return PodioRestHelper.JSONRequest<PodioCollection<Item>>(Constants.PODIOAPI_BASEURL + "/item/app/" + appId + "/filter/", _client.AuthInfo.AccessToken, requestData, PodioRestHelper.RequestMethod.POST).Data;
        }

        [DataContract]
        public struct CreateUpdateRequest
        {
            [DataMember(IsRequired = false, Name = "fields")]
            public IEnumerable<IDictionary<string, object>> Fields { get; set; }

            [DataMember(IsRequired = false, Name = "file_ids")]
            public IEnumerable<int> FileIds { get; set; }

            [DataMember(IsRequired = false, Name = "tags")]
            public IEnumerable<string> Tags { get; set; }

            [DataMember(IsRequired = false, Name = "reminder")]
            public Reminder Reminder { get; set; }

            [DataMember(IsRequired = false, Name = "recurrence")]
            public Recurrence Recurrence { get; set; }

            [DataMember(IsRequired = false, Name = "linked_account_id")]
            public int? LinkedAccountId { get; set; }

            [DataMember(IsRequired = false, Name = "ref")]
            public Ref Ref { get; set; }
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/add-new-item-22362
        /// </summary>
        public int AddNewItem(int appId, Item item) {
            var fieldValues = item.Fields.Select(f => f.Values == null ? null : new { external_id = f.ExternalId, values = f.Values }.AsDictionary()).Where(f => f != null);
            var requestData = new CreateUpdateRequest()
            {
                Fields = fieldValues,
                FileIds = item.FileIds,
                Tags = item.Tags.Select(tag => tag.Text)
            };
            var newItem = AddNewItem(appId, requestData);
            item.ItemId = newItem.ItemId;
            item.Title = newItem.Title;
            return (int)item.ItemId;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/add-new-item-22362
        /// </summary>
        public Item AddNewItem(int appId, CreateUpdateRequest requestData)
        {
            return PodioRestHelper.JSONRequest<Item>(Constants.PODIOAPI_BASEURL + "/item/app/" + appId + "/", _client.AuthInfo.AccessToken, requestData, PodioRestHelper.RequestMethod.POST).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/update-item-22363
        /// </summary>
        public void UpdateItem(Item item)
        {
            var fieldValues = item.Fields.Select(f => f.Values == null ? null : new { external_id = f.ExternalId, values = f.Values }.AsDictionary()).Where(f => f != null);
            var requestData = new CreateUpdateRequest()
            {
                Fields = fieldValues,
                FileIds = item.FileIds,
                Tags = item.Tags.Select(tag => tag.Text),

            };
            UpdateItem((int)item.ItemId, requestData);
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/update-item-22363
        /// </summary>
        public void UpdateItem(int itemId, CreateUpdateRequest requestData)
        {
            PodioRestHelper.JSONRequest<Item>(Constants.PODIOAPI_BASEURL + "/item/" + itemId, _client.AuthInfo.AccessToken, requestData, PodioRestHelper.RequestMethod.PUT);
        }
        /// <summary>
        /// https://developers.podio.com/doc/items/delete-item-s-22364
        /// </summary>
        public PodioRestHelper.PodioResponse DeleteItems(IEnumerable<int> itemIds, bool silent = false)
        {
            return PodioRestHelper.JSONRequest(Constants.PODIOAPI_BASEURL + "/item/" + String.Join(",", itemIds.Select(id => id.ToString()).ToArray()), _client.AuthInfo.AccessToken, new { silent = silent }, PodioRestHelper.RequestMethod.DELETE);
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/delete-item-s-22364
        /// </summary>
        public PodioRestHelper.PodioResponse DeleteItem(int itemId, bool silent = false)
        {
            return DeleteItems(new int[] { itemId }, silent);
        }

        /*
         * 
Calculate
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
