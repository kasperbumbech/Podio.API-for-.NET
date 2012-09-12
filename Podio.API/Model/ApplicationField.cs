using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class ApplicationField 
	{


		[DataMember(Name = "field_id", IsRequired=false)]
		public int? FieldId { get; set; }


		[DataMember(Name = "type", IsRequired=false)]
		public string Type { get; set; }


		[DataMember(Name = "external_id", IsRequired=false)]
		public string ExternalId { get; set; }


        //[DataMember(Name = "config", IsRequired=false)]
        //public  Config { get; set; }


		[DataMember(Name = "status", IsRequired=false)]
		public string Status { get; set; }


	}
}

