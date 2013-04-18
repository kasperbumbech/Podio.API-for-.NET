using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Podio.API.Model
{
    [DataContract]
    public class ItemFieldRange
    {
        [DataMember(Name = "min", IsRequired = false)]
        public int? Min { get; set; }

        [DataMember(Name = "max", IsRequired = false)]
        public int? Max { get; set; }
    }
}
