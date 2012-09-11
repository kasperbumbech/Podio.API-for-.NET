using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Podio.API.Model
{
    [DataContract]
    public class Organisation
    {
        [DataMember(Name = "status", IsRequired=false)]
        public string Status { get; set; }

        [DataMember(Name = "premium", IsRequired = false)]
        public bool Premium { get; set; }

        [DataMember(Name = "url_label", IsRequired = false)]
        public string UrlLabel { get; set; }

        [DataMember(Name = "image", IsRequired = false)]
        public Image Image { get; set; }

        [DataMember(Name = "rank", IsRequired = false)]
        public int Rank { get; set; }

        [DataMember(Name = "spaces", IsRequired = false)]
        public IList<Space> Spaces { get; set; }

        [DataMember(Name = "logo", IsRequired = false)]
        public int Logo { get; set; }

        [DataMember(Name = "segment", IsRequired = false)]
        public object Segment { get; set; }

        [DataMember(Name = "contract_status", IsRequired = false)]
        public string ContractStatus { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "rights", IsRequired = false)]
        public IList<string> Rights { get; set; }

        [DataMember(Name = "url", IsRequired = false)]
        public string Url { get; set; }

        [DataMember(Name = "org_id")]
        public int OrgId { get; set; }

        [DataMember(Name = "role", IsRequired = false)]
        public string Role { get; set; }

        [DataMember(Name = "domains", IsRequired = false)]
        public IList<string> Domains { get; set; }

        [DataMember(Name = "type", IsRequired = false)]
        public string Type { get; set; }

        [DataMember(Name = "segment_size", IsRequired = false)]
        public object SegmentSize { get; set; }

        
    }
}
