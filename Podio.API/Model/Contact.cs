using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Podio.API.Model
{
    [DataContract]
    public class ContactV2
    {
        [DataMember(IsRequired=false,Name = "mail")]
        public IList<string> Mail { get; set; }

        [DataMember(IsRequired=false,Name = "image")]
        public Image Image { get; set; }

        [DataMember(IsRequired=false,Name = "profile_id")]
        public int ProfileId { get; set; }

        [DataMember(IsRequired=false,Name = "phone")]
        public IList<string> Phone { get; set; }

        [DataMember(IsRequired=false,Name = "link")]
        public string Link { get; set; }

        [DataMember(IsRequired=false,Name = "skype")]
        public string Skype { get; set; }

        [DataMember(IsRequired=false,Name = "skill")]
        public IList<string> Skill { get; set; }

        [DataMember(IsRequired=false,Name = "city")]
        public string City { get; set; }

        [DataMember(IsRequired=false,Name = "about")]
        public string About { get; set; }

        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(IsRequired=false,Name = "zip")]
        public string Zip { get; set; }

        [DataMember(IsRequired=false,Name = "rights")]
        public IList<string> Rights { get; set; }

        [DataMember(IsRequired=false,Name = "url")]
        public IList<string> Url { get; set; }

        [DataMember(IsRequired=false,Name = "type")]
        public string Type { get; set; }

        [DataMember(IsRequired=false,Name = "last_seen_on")]
        public string LastSeenOn { get; set; }

        [DataMember(IsRequired=false,Name = "avatar")]
        public int Avatar { get; set; }

        [DataMember(IsRequired=false,Name = "country")]
        public string Country { get; set; }

        [DataMember(IsRequired=false,Name = "im")]
        public IList<string> Im { get; set; }

        [DataMember(IsRequired=false,Name = "external_id")]
        public string ExternalId { get; set; }

        [DataMember(IsRequired=false,Name = "twitter")]
        public string Twitter { get; set; }

        [DataMember(IsRequired=false,Name = "address")]
        public IList<string> Address { get; set; }
    }

}
