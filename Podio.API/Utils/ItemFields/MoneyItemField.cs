using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Podio.API.Model;

namespace Podio.API.Utils.ItemFields
{
    public class MoneyItemField : ItemField
    {
        public string Currency
        {
            get
            {
                if (this.HasValue("currency"))
                {
                    return (string)this.Values.First()["currency"];
                }
                else
                {
                    return null;
                }
            }
        }

        public decimal? Value
        {
            get
            {
                if (this.HasValue("value"))
                {
                    return Decimal.Parse((string)this.Values.First()["value"]);
                }
                else
                {
                    return null;
                }
            }
        }

    }

}
