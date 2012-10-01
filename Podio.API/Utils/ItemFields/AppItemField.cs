using Podio.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podio.API.Utils.ItemFields
{
    public class AppItemField : ItemField
    {
        private List<Item> _items;

        public List<Item> Items
        {
            get
            {
                return this.valuesAs<Item>(_items);
            }
        }
    }
}
