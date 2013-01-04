using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;

namespace Podio.API.Model
{
    [DataContract]
    public class AppValues
    {
        [DataMember(Name = "created_bys", IsRequired = false)]
        public List<Contact> Creators { get; set; }

        [DataMember(Name = "created_vias", IsRequired = false)]
        public List<Via> CreatedVias { get; set; }

        [DataMember(Name = "tags", IsRequired = false)]
        public List<string> Tags { get; set; }

        [DataMember(Name = "fields", IsRequired = false)]
        public List<ItemField> Fields { get; set; }
    }
}
