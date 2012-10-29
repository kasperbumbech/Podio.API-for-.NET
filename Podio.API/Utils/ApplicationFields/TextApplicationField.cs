using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Podio.API.Model;

namespace Podio.API.Utils.ApplicationFields
{
    public class TextApplicationField : ApplicationField
    {
        public string Size {
            get {
                return (string)this.GetSetting("size");
            }
        }
    }
}
