using Podio.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podio.API.Model
{
    public partial class ItemField
    {
        public bool HasValue(string key = null) {
            return this.Values != null
                && this.Values.Count > 0
                && (key == null || 
                (this.Values.First() != null &&
                this.Values.First().ContainsKey(key)));
        }

        protected T valueAs<T>(Dictionary<string,object> value, string key)
            where T : class, new()
        {
            return ((Dictionary<string,object>)value[key]).As<T>();
        }

        protected List<T> valuesAs<T>(List<T> list)
            where T : class, new()
        {
            if (list == null)
            {
                list = new List<T>();
                foreach (var itemAttributes in this.Values)
                {
                    var obj = this.valueAs<T>(itemAttributes, "value");
                    list.Add(obj);
                }
            }
            return list;        
        }
    }
}
