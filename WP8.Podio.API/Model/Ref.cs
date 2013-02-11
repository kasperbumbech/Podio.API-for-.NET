using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WP8.Podio.API.Model
{
    [DataContract(Name = "ref")]
    public class Ref
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "id")]
        public int? Id { get; set; }
    }
}
