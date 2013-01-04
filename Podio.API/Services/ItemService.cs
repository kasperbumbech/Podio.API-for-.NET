using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Podio.API.Model;
using Podio.API.Utils;


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
        /// https://developers.podio.com/doc/items/add-new-item-22362
        /// </summary>
        public Item AddNewItem(int appId, CreateUpdateRequest requestData)
        {
            return PodioRestHelper.JSONRequest<Item>(String.Format("{0}/item/app/{1}/", Constants.PODIOAPI_BASEURL, appId), _client.AuthInfo.AccessToken, requestData, PodioRestHelper.RequestMethod.POST).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/add-new-item-22362
        /// </summary>
        public int AddNewItem(int appId, Item item)
        {
            var fieldValues = item.Fields.Select(f => f.Values == null ? null : new { external_id = f.ExternalId, values = f.Values }.AsDictionary()).Where(f => f != null);
            var requestData = new CreateUpdateRequest()
            {
                ExternalId = item.ExternalId,
                Fields = fieldValues,
                FileIds = item.FileIds,
                Tags = item.Tags
            };
            var newItem = AddNewItem(appId, requestData);
            item.ItemId = newItem.ItemId;
            item.Title = newItem.Title;
            return (int)item.ItemId;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/calculate-67633
        /// </summary>
        public void Calculate(int appId)
        {
            throw new NotImplementedException("Method not implemented yet.");
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/delete-item-s-22364
        /// </summary>
        public PodioRestHelper.PodioResponse DeleteItem(int itemId, bool silent = false)
        {
            return DeleteItems(new int[] { itemId }, silent);
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/delete-item-reference-7302326
        /// </summary>
        public PodioRestHelper.PodioResponse DeleteItemReference(int itemId)
        {
            return PodioRestHelper.JSONRequest(String.Format("{0}/item/{1}/ref", Constants.PODIOAPI_BASEURL, itemId), _client.AuthInfo.AccessToken, null, PodioRestHelper.RequestMethod.DELETE);
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/delete-item-s-22364
        /// </summary>
        public PodioRestHelper.PodioResponse DeleteItems(IEnumerable<int> itemIds, bool silent = false)
        {
            return PodioRestHelper.JSONRequest(String.Format("{0}/item/{1}", Constants.PODIOAPI_BASEURL, String.Join(",", itemIds.Select(id => id.ToString()).ToArray())), _client.AuthInfo.AccessToken, new { silent = silent }, PodioRestHelper.RequestMethod.DELETE);
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/export-items-4235696
        /// </summary>
        public Batch ExportItems(int appId, ExportType export, FilterItemRequest request)
        {
            return PodioRestHelper.JSONRequest<Batch>(String.Format("{0}/item/app/{1}/export/{2}", Constants.PODIOAPI_BASEURL, appId, export), _client.AuthInfo.AccessToken, request, PodioRestHelper.RequestMethod.POST).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/filter-items-4496747
        /// </summary>
        public PodioCollection<Item> FilterItems(int appId, FilterItemRequest requestData)
        {
            return PodioRestHelper.JSONRequest<PodioCollection<Item>>(String.Format("{0}/item/app/{1}/filter/", Constants.PODIOAPI_BASEURL, appId), _client.AuthInfo.AccessToken, requestData, PodioRestHelper.RequestMethod.POST).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/filter-items-by-view-4540284
        /// </summary>
        public PodioCollection<Item> FilterItemsByView(int appId, int viewId, int? limit = null, int? offset = null, bool? remembered = null)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();

            if (limit != null)
                args.Add("limit", limit.ToString());
            if (offset != null)
                args.Add("offset", offset.ToString());

            if (remembered != null)
                args.Add("remembered", remembered.ToString());

            return PodioRestHelper.Request<PodioCollection<Item>>(String.Format("{0}/item/app/{1}/filter/{2}/", Constants.PODIOAPI_BASEURL, appId, viewId), _client.AuthInfo.AccessToken, args, PodioRestHelper.RequestMethod.POST).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/find-referenceable-items-22485
        /// </summary>
        public List<Item> FindReferenceableItems(int fieldId, int? limit = null, IEnumerable<int> excludeItemIds =null, string sort =null, string search = null)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            if (limit != null) { args.Add("limit", limit.ToString()); }
            if (sort != null) { args.Add("sort", sort); }
            if (search != null) { args.Add("text", search); }
            if (excludeItemIds != null)
            {
                args.Add("not_item_id", String.Join(",", excludeItemIds.Select(id => id.ToString()).ToArray()));
            }
            return PodioRestHelper.Request<List<Item>>(String.Format("{0}/item/field/{1}/find", Constants.PODIOAPI_BASEURL, fieldId), _client.AuthInfo.AccessToken, args, PodioRestHelper.RequestMethod.GET).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-app-values-22455
        /// </summary>
        public AppValues GetAppValues(int appId)
        {
            return PodioRestHelper.Request<AppValues>(String.Format("{0}/item/app/{1}/values", Constants.PODIOAPI_BASEURL, appId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-field-ranges-24242866
        /// </summary>
        public ItemFieldRange GetFieldRanges(int fieldId)
        {
            return PodioRestHelper.Request<ItemFieldRange>(String.Format("{0}/item/field/{1}/range", Constants.PODIOAPI_BASEURL, fieldId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-item-22360
        /// </summary>
        public Item GetItem(int itemId, bool markAsViewed = true)
        {
            Dictionary<string, string> args = new Dictionary<string, string>() { { "mark_as_viewed", markAsViewed.ToString() } };
            return PodioRestHelper.Request<Item>(String.Format("{0}/item/{1}", Constants.PODIOAPI_BASEURL, itemId), _client.AuthInfo.AccessToken, args).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-item-basic-61768
        /// </summary>
        public Item GetItemBasic(int itemId, bool markAsViewed = true)
        {
            Dictionary<string, string> args = new Dictionary<string, string>() { { "mark_as_viewed", markAsViewed.ToString() } };
            return PodioRestHelper.Request<Item>(String.Format("{0}/item/{1}/basic", Constants.PODIOAPI_BASEURL, itemId), _client.AuthInfo.AccessToken, args).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-item-by-external-id-19556702
        /// </summary>
        public Item GetItemByExternalId(int appId, int externalId)
        {
            return PodioRestHelper.Request<Item>(String.Format("{0}/item/app/{1}/external_id/{2}", Constants.PODIOAPI_BASEURL, appId, externalId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-item-field-values-22368
        /// </summary>
        public List<Dictionary<string, object>> GetItemFieldValues(int itemId, int fieldId)
        {
            return PodioRestHelper.Request<List<Dictionary<string, object>>>(String.Format("{0}/item/{1}/value/{2}", Constants.PODIOAPI_BASEURL, itemId, fieldId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-item-preview-for-field-reference-7529318
        /// </summary>
        public Item GetItemPreviewForFieldReference(int itemId, int fieldId)
        {
            return PodioRestHelper.Request<Item>(String.Format("{0}/item/{1}/reference/{2}/preview", Constants.PODIOAPI_BASEURL, itemId, fieldId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-item-references-22439
        /// </summary>
        public List<ItemReference> GetItemReferences(int itemId)
        {
            return PodioRestHelper.Request<List<ItemReference>>(String.Format("{0}/item/{1}/reference/", Constants.PODIOAPI_BASEURL, itemId), _client.AuthInfo.AccessToken, null, PodioRestHelper.RequestMethod.GET).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-references-to-item-by-field-7403920
        /// </summary>
        public List<Item> GetItemReferencesByField(int itemId, int fieldId, int? limit = null)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            if (limit != null) { args.Add("limit", limit.ToString()); }

            return PodioRestHelper.Request<List<Item>>(String.Format("{0}/item/{1}/reference/field/{2}", Constants.PODIOAPI_BASEURL, itemId, fieldId), _client.AuthInfo.AccessToken, args, PodioRestHelper.RequestMethod.GET).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-item-revision-22373
        /// </summary>
        public ItemRevision GetItemRevision(int itemId, int revisionId)
        {
            return PodioRestHelper.Request<ItemRevision>(String.Format("{0}/item/{1}/revision/{2}", Constants.PODIOAPI_BASEURL, itemId, revisionId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-item-revision-difference-22374
        /// </summary>
        public ItemDiff GetItemRevisionDifference(int itemId, int revisionFromId, int revisionToId)
        {
            return PodioRestHelper.Request<ItemDiff>(String.Format("{0}/item/{1}/revision/{2}/{3}", Constants.PODIOAPI_BASEURL, itemId, revisionFromId, revisionToId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-item-revision-difference-22374
        /// </summary>
        public ICollection<ItemRevision> GetItemRevisions(int itemId)
        {
            return PodioRestHelper.Request<ICollection<ItemRevision>>(String.Format("{0}/item/{1}/revision/", Constants.PODIOAPI_BASEURL, itemId), _client.AuthInfo.AccessToken).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-items-27803
        /// </summary>
        public PodioCollection<Item> GetItems(int appId, int limit, int offset, Dictionary<string, string> filters = null, bool? remembered = null, string sortBy = null, bool? sortDesc = null, int? viewId = null)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("limit", limit.ToString());
            args.Add("offset", offset.ToString());

            if (remembered != null) { args.Add("remembered", remembered.ToString()); }
            if (!string.IsNullOrEmpty(sortBy)) { args.Add("sort_by", sortBy); }
            if (sortDesc != null) { args.Add("sort_desc", sortDesc.ToString()); }
            if (viewId != null) { args.Add("view_id", viewId.ToString()); }

            if (filters != null)
            {
                foreach (KeyValuePair<string, string> f in filters)
                {
                    if (!args.ContainsKey(f.Key))
                    {
                        args.Add(f.Key, f.Value);
                    }
                }
            }

            return PodioRestHelper.Request<PodioCollection<Item>>(String.Format("{0}/item/app/{1}/", Constants.PODIOAPI_BASEURL, appId), _client.AuthInfo.AccessToken, args).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-items-as-xlsx-63233
        /// </summary>
        public void GetItemsAsXLSX(int appId, FilterItemRequest req)
        {
            throw new NotImplementedException("Method not implemented yet.");
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-meeting-url-14763260
        /// </summary>
        public string GetMeetingURL(int itemId)
        {
            string retval = String.Empty;
            Dictionary<string, object> o = PodioRestHelper.JSONRequest<Dictionary<string, object>>(String.Format("{0}/item/{1}/meeting/url", Constants.PODIOAPI_BASEURL, itemId), _client.AuthInfo.AccessToken, null, PodioRestHelper.RequestMethod.GET).Data;
            if (o.ContainsKey("url")) { retval = o["url"].ToString(); }
            return retval;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/get-top-values-for-field-68334
        /// </summary>
        public List<Item> GetTopValuesByField(int fieldId, int? limit = null, IEnumerable<int> excludeItemIds = null)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            if (limit != null)
            {
                args.Add("limit", limit.ToString());
            }
            if (excludeItemIds != null)
            {
                args.Add("not_item_id", String.Join(",", excludeItemIds.Select(id => id.ToString()).ToArray()));
            }
            return PodioRestHelper.Request<List<Item>>(String.Format("{0}/item/field/{1}/top", Constants.PODIOAPI_BASEURL, fieldId), _client.AuthInfo.AccessToken, args, PodioRestHelper.RequestMethod.GET).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/revert-item-revision-953195
        /// </summary>
        public ItemRevision RevertItemRevision(int itemId, int revisionId)
        {
            return PodioRestHelper.Request<ItemRevision>(String.Format("{0}/item/{1}/revision/{2}", Constants.PODIOAPI_BASEURL, itemId, revisionId), _client.AuthInfo.AccessToken, null, PodioRestHelper.RequestMethod.DELETE).Data;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/set-participation-7156154
        /// </summary>
        public PodioRestHelper.PodioResponse SetParticipation(int itemId, ParticipationStatus status)
        {
            Dictionary<string, string> args = new Dictionary<string,string>(){{"status",status.ToString() }};
            return PodioRestHelper.Request(String.Format("{0}/item/{1}/participation", Constants.PODIOAPI_BASEURL, itemId), _client.AuthInfo.AccessToken, args, PodioRestHelper.RequestMethod.PUT);
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/update-item-22363
        /// </summary>
        public void UpdateItem(Item item)
        {
            var fieldValues = item.Fields.Select(f => f.Values == null ? null : new { external_id = f.ExternalId, values = f.Values }.AsDictionary()).Where(f => f != null);
            var requestData = new CreateUpdateRequest()
            {
                ExternalId = item.ExternalId,
                Fields = fieldValues,
                FileIds = item.FileIds,
                Tags = item.Tags
            };
            UpdateItem((int)item.ItemId, requestData);
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/update-item-22363
        /// </summary>
        public void UpdateItem(int itemId, CreateUpdateRequest requestData)
        {
            PodioRestHelper.JSONRequest<Item>(String.Format("{0}/item/{1}", Constants.PODIOAPI_BASEURL, itemId), _client.AuthInfo.AccessToken, requestData, PodioRestHelper.RequestMethod.PUT);
        }
        /// <summary>
        /// https://developers.podio.com/doc/items/update-item-field-values-22367
        /// </summary>
        public void UpdateItemFieldValues(int itemId, int fieldId, bool silent = false)
        {
            throw new NotImplementedException("Method not implemented yet.");
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/update-item-values-22366
        /// </summary>
        public void UpdateItemValues(int itemId)
        {
            throw new NotImplementedException("Method not implemented yet.");
        }

        [DataContract]
        public struct CreateUpdateRequest
        {
            [DataMember(IsRequired = false, Name = "external_id")]
            public string ExternalId { get; set; }

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
        [DataContract]
        public struct FilterItemRequest
        {
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

            [DataMember(IsRequired = false, Name = "filters")]
            public List<Dictionary<string, object>> Filters { get; set; }
        }

    }
}
