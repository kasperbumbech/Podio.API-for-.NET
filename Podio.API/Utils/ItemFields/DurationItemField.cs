using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Podio.API.Model;

namespace Podio.API.Utils.ItemFields
{
    public class DurationItemField : ItemField
    {
        public TimeSpan? Value
        {
            get
            {
                if (this.HasValue("value"))
                {
                    return TimeSpan.FromSeconds(Convert.ToDouble((Int64)this.Values.First()["value"]));
                }
                else
                {
                    return null;
                }
            }
        }

    }

}
