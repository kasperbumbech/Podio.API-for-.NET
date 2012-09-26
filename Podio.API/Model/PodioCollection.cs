using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Podio.API.Model
{
    [DataContract(Name="PodioCollection")]
    public class PodioCollection<T>
    {
        [DataMember(Name = "items", IsRequired = false)]
        public IEnumerable<T> Items { get; set; }

        [DataMember(Name = "filtered", IsRequired = false)]
        public int Filtered { get; set; }

        [DataMember(Name = "total", IsRequired = false)]
        public int Total { get; set; }
    }
}
