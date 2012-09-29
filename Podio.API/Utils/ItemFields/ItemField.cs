using Podio.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podio.API.Model
{
    public partial class ItemField
    {
        public bool HasValue() {
            return this.Values != null && this.Values.Count > 0;
        }
    }
}
