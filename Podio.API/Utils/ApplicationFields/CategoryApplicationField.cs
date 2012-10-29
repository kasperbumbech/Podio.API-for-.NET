using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Podio.API.Model;
using Podio.API.Utils.ItemFields;
using System.Runtime.Serialization;

namespace Podio.API.Utils.ApplicationFields
{
    public class CategoryApplicationField : ApplicationField
    {
        IEnumerable<CategoryItemField.Answer> _options;

        public IEnumerable<CategoryItemField.Answer> Options
        {
            get
            {
                if(_options == null) {
                    _options = this.GetSettingsAs<CategoryItemField.Answer>("options");
                }
                return _options;
            }
        }

        public bool Multiple
        {
            get
            {
                return (bool)this.GetSetting("multiple");
            }
        }
    }
}
