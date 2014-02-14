using System.Runtime.Serialization;
using Podio.API.Model;
using Podio.API.Utils;

namespace Podio.API.Services
{
    public class ContactService
    {
        private Client _client;
        /// <summary>
        /// Add a client and you can use this as a shortcut to the Podio REST API 
        /// </summary>
        public ContactService(Client client)
        {
            _client = client;
        }


        // TODO
        [DataContract]
        public struct CreateUpdateRequest
        {
            [DataMember(IsRequired = false, Name = "name")]
            public string Name { get; set; }

            [DataMember(IsRequired = false, Name = "address")]
            public string[] Address { get; set; }

            [DataMember(IsRequired = false, Name = "city")]
            public string City { get; set; }

            [DataMember(IsRequired = false, Name = "state")]
            public string State { get; set; }

            [DataMember(IsRequired = false, Name = "zip")]
            public string Zip { get; set; }

            [DataMember(IsRequired = false, Name = "country")]
            public string Country { get; set; }

            [DataMember(IsRequired = false, Name = "mail")]
            public string[] Mail { get; set; }

            [DataMember(IsRequired = false, Name = "phone")]
            public string[] Phone { get; set; }

            [DataMember(IsRequired = false, Name = "url")]
            public string[] Url { get; set; }
        }




        /// <summary>
        /// https://developers.podio.com/doc/items/add-new-item-22362
        /// </summary>
        public int CreateSpaceContact(int spaceId, Contact contact)
        {
            var requestData = new CreateUpdateRequest
            {
                Name = contact.Name,
                Address = contact.Address,
                City = contact.City,
                State = contact.State,
                Zip = contact.Zip,
                Country = contact.Country,
                Mail = contact.Mail,
                Phone = contact.Phone,
                Url = contact.Url
            };
            var newContact = CreateSpaceContact(spaceId, requestData);
            contact.ProfileId = newContact.ProfileId;
            return (int)contact.ProfileId;
        }

        /// <summary>
        /// https://developers.podio.com/doc/items/add-new-item-22362
        /// </summary>
        public Contact CreateSpaceContact(int spaceId, CreateUpdateRequest requestData)
        {
            return PodioRestHelper.JSONRequest<Contact>(Constants.PODIOAPI_BASEURL + "/contact/space/" + spaceId + "/", _client.AuthInfo.AccessToken, requestData, PodioRestHelper.RequestMethod.POST).Data;
        }
    }
}
