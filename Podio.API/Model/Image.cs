using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Podio.API.Model
{
    [DataContract]
    public class Image
    {
        [DataMember(Name = "perma_link")]
        public string PermaLink { get; set; }

        [DataMember(IsRequired=false,Name = "hosted_by")]
        public string HostedBy { get; set; }

        [DataMember(IsRequired=false,Name = "hosted_by_humanized_name")]
        public string HostedByHumanizedName { get; set; }

        [DataMember(IsRequired=false,Name = "thumbnail_link")]
        public string ThumbnailLink { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "file_id")]
        public int FileId { get; set; }
    }
}

