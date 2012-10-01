using Podio.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podio.API.Utils.ItemFields
{
    public class LocationItemField : ItemField
    {
        public List<string> Locations
        {
            get
            {
                return new List<string>(this.Values.Select(s => (string)s["value"]));
            }
        }
    }
}
