using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Podio.API.Model;
using Podio.API.Utils;


namespace Podio.API.Services
{
    public class UserService
    {
        private Client _client;
        /// <summary>
        /// Add a client and you can use this as a shortcut to the Podio REST API 
        /// </summary>
        public UserService(Client client)
        {
            _client = client;
        }

        public Contact GetUserContact(string mail)
        {
            Contact contact = null;
            Dictionary<string, string> _args = new Dictionary<string, string>();
            _args.Add("mail", mail);
            _args.Add("contact_type", "user");
            _args.Add("limit", "1");

            List<Contact> contacts = PodioRestHelper.Request<List<Contact>>(Constants.PODIOAPI_BASEURL + "/contact/", _client.AuthInfo.AccessToken, _args).Data;
            if (contacts != null && contacts.Count > 0)
            {
                return contacts[0];
            }
            return contact;
        }

    }
}
