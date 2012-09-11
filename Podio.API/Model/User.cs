using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Podio.API.Model
{
    [DataContract]
    public class User
    {
        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(IsRequired=false,Name = "rights")]
        public IList<string> Rights { get; set; }

        [DataMember(IsRequired=false,Name = "external_id")]
        public string ExternalId { get; set; }

        [DataMember(IsRequired=false,Name = "image")]
        public Image Image { get; set; }

        [DataMember(IsRequired=false,Name = "profile_id")]
        public int ProfileId { get; set; }

        [DataMember(IsRequired=false,Name = "link")]
        public string Link { get; set; }

        [DataMember(IsRequired=false,Name = "removable")]
        public bool Removable { get; set; }

        [DataMember(IsRequired=false,Name = "mail")]
        public IList<string> Mail { get; set; }

        [DataMember(IsRequired=false,Name = "role")]
        public IList<string> Role { get; set; }

        [DataMember(IsRequired=false,Name = "type")]
        public string Type { get; set; }

        [DataMember(IsRequired=false,Name = "last_seen_on")]
        public string LastSeenOn { get; set; }
    }
}