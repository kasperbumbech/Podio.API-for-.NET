using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Podio.API.Model;
using Podio.API.Utils.ItemFields;
using System.Runtime.Serialization;

namespace Podio.API.Utils.ApplicationFields
{
    public class StateApplicationField : ApplicationField
    {
        IEnumerable<string> _allowed_values;

        public IEnumerable<string> AllowedValues
        {
            get
            {
                if (_allowed_values == null)
                {
                    _allowed_values = this.GetSettingsAs<string>("allowed_values");
                }
                return _allowed_values;
            }
        }
    }
}
