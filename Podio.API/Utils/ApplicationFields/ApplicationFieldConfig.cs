using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Podio.API.Model
{
    [DataContract(Name = "ApplicationFieldConfig")]
    public class ApplicationFieldConfig
    {
        [DataMember(Name = "label", IsRequired = false)]
        public string Label { get; set; }

        [DataMember(Name = "description", IsRequired = false)]
        public string Filtered { get; set; }

        [DataMember(Name = "delta", IsRequired = false)]
        public int? Delta { get; set; }
    
        [DataMember(Name = "settings", IsRequired = false)]
        public Dictionary<string, object> Settings { get; set; }

        [DataMember(Name = "mapping", IsRequired = false)]
        public string Mapping { get; set; }

        [DataMember(Name = "required", IsRequired = false)]
        public bool? Required { get; set; }
    }
}
