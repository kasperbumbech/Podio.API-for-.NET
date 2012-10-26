using Podio.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podio.API.Utils.ItemFields
{
    public class ContactItemField : ItemField
    {
        private List<Contact> _contacts;

        public IEnumerable<Contact> Contacts
        {
            get
            {
                return this.valuesAs<Contact>(_contacts);
            }
        }

        public IEnumerable<int> ContactIds
        {
            set
            {
                ensureValuesInitialized();
                foreach (var contactId in value)
                {
                    var dict = new Dictionary<string, object>();
                    dict["value"] = contactId;
                    this.Values.Add(dict);
                }
            }
        }
    }
}
