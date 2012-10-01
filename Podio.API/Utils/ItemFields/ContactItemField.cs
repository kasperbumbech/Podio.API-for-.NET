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

        public List<Contact> Contacts
        {
            get
            {
                return this.valuesAs<Contact>(_contacts);
            }
        }
    }
}
