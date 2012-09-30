using Podio.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podio.API.Utils.ItemFields
{
    public class TextItemField : ItemField
    {
        public string Value {
            get {
                if (this.HasValue("value"))
                {
                    return (string)this.Values.First()["value"];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
