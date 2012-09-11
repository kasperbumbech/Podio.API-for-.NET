using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Podio.API.Model
{
    [DataContract]
    public class Space
    {
        [DataMember(IsRequired=false,Name = "premium")]
        public bool Premium { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(IsRequired=false,Name = "rights")]
        public IList<string> Rights { get; set; }

        [DataMember(IsRequired=false,Name = "url")]
        public string Url { get; set; }

        [DataMember(IsRequired=false,Name = "url_label")]
        public string UrlLabel { get; set; }

        [DataMember(Name = "space_id")]
        public int SpaceId { get; set; }

        [DataMember(IsRequired=false,Name = "role")]
        public string Role { get; set; }

        [DataMember(IsRequired=false,Name = "type")]
        public string Type { get; set; }

        [DataMember(IsRequired=false,Name = "rank")]
        public int Rank { get; set; }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/create-space-22390
        /// </summary>
        [DataContract]
        public struct CreatedResponse
        {
            [DataMember(Name = "space_id")]
            public int SpaceId { get; set; }

            [DataMember(Name = "url")]
            public string Url { get; set; }
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/create-space-22390
        /// </summary>
        [DataContract]
        public struct CreateRequest
        {
            [DataMember(IsRequired=false,Name = "org_id")]
            public int OrgId { get; set; }

            [DataMember(IsRequired=false,Name = "privacy")]
            public string Privacy { get; set; }

            [DataMember(IsRequired=false,Name = "auto_join")]
            public bool AutoJoin { get; set; }

            [DataMember(IsRequired=false,Name = "name")]
            public string Name { get; set; }

            [DataMember(IsRequired=false,Name = "post_on_new_app")]
            public bool PostOnNewApp { get; set; }

            [DataMember(IsRequired=false,Name = "post_on_new_member")]
            public bool PostOnNewMember { get; set; }
        }

        /// <summary>
        /// https://developers.podio.com/doc/spaces/update-space-22391
        /// </summary>
        [DataContract]
        public struct UpdateRequest
        {
            [DataMember(IsRequired = false, Name = "privacy")]
            public string Privacy { get; set; }

            [DataMember(IsRequired = false, Name = "auto_join")]
            public bool AutoJoin { get; set; }

            [DataMember(IsRequired = false, Name = "name")]
            public string Name { get; set; }

            [DataMember(IsRequired = false, Name = "post_on_new_app")]
            public bool PostOnNewApp { get; set; }

            [DataMember(IsRequired = false, Name = "post_on_new_member")]
            public bool PostOnNewMember { get; set; }
        }
    }
}
