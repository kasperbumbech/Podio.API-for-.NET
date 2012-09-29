using Podio.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podio.API.Utils.ItemFields
{
    public class TextItemField : ItemField
    {
        public string Value() {
            if (this.HasValue()) {
                return (string)this.Values.First()["value"];
            } else {
                return null;
            }
        }
    }
}
