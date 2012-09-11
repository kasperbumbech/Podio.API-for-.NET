using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Podio.API.Model
{
    [DataContract]
    public class App
    {
        [DataMember(IsRequired=false,Name = "status")]
        public string Status { get; set; }

        [DataMember(IsRequired=false,Name = "link_add")]
        public string LinkAdd { get; set; }

        [DataMember(IsRequired=false,Name = "date_field")]
        public bool DateField { get; set; }

        [DataMember(IsRequired=false,Name = "link")]
        public string Link { get; set; }

        [DataMember(IsRequired=false,Name = "url")]
        public string Url { get; set; }

        [DataMember(IsRequired=false,Name = "url_label")]
        public string UrlLabel { get; set; }

        [DataMember(IsRequired=false,Name = "space_id")]
        public int SpaceId { get; set; }

        [DataMember(IsRequired=false,Name = "config")]
        public AppConfig Config { get; set; }

        [DataMember(IsRequired=false,Name = "url_add")]
        public string UrlAdd { get; set; }

        [DataMember(IsRequired=false,Name = "app_id")]
        public int AppId { get; set; }

        [DataContract]
        public class AppConfig
        {
            [DataMember(IsRequired=false,Name = "allow_edit")]
            public bool AllowEdit { get; set; }

            [DataMember(IsRequired=false,Name = "description")]
            public string Description { get; set; }

            [DataMember(IsRequired=false,Name = "item_name")]
            public string ItemName { get; set; }

            [DataMember(IsRequired=false,Name = "type")]
            public string Type { get; set; }

            [DataMember(IsRequired=false,Name = "icon_id")]
            public int IconId { get; set; }

            [DataMember(IsRequired=false,Name = "allow_create")]
            public bool AllowCreate { get; set; }

            [DataMember(IsRequired=false,Name = "usage")]
            public string Usage { get; set; }

            [DataMember(IsRequired=false,Name = "icon")]
            public string Icon { get; set; }

            [DataMember(IsRequired=false,Name = "external_id")]
            public object ExternalId { get; set; }

            [DataMember(IsRequired=false,Name = "name")]
            public string Name { get; set; }
        }
    }
}
