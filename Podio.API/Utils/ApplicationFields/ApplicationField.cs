using Podio.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podio.API.Model
{
    public partial class ApplicationField
    {
        public object GetSetting(string key)
        {
            if (this.Config.Settings != null)
            {
                if (Config.Settings.ContainsKey(key))
                {
                    return Config.Settings[key];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<T> GetSettingsAs<T>(string key) {
            var rawOptions = (Newtonsoft.Json.Linq.JArray)this.GetSetting(key);
            var options = new T[rawOptions.Count];
            for (int i = 0; i < rawOptions.Count; i++)
            {
                options[i] = rawOptions[i].ToObject<T>();
            }
            return options;
        }
    }
}
