using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Podio.API.Model
{
    [DataContract]
    public class ItemExpanded : Item
    {

        [DataMember(Name = "files", IsRequired = false)]
        public List<FileAttachment> Files { get; set; }

        [DataMember(Name = "comments", IsRequired = false)]
        public List<Comment> Comments { get; set; }


    }
}
