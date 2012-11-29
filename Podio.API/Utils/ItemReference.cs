using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Podio.API.Model
{
    [DataContract]
    public partial class ItemReference
    {
        [DataMember(Name = "field", IsRequired = false)]
        public ItemField Field { get; set; }

        [DataMember(Name = "app", IsRequired = false)]
        public Application App { get; set; }

        [DataMember(Name = "items", IsRequired = false)]
        public List<Item> Items { get; set; }
    }
}
