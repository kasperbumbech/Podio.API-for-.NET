using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class Hook 
	{


		[DataMember(Name = "hook_id", IsRequired=false)]
		public int? HookId { get; set; }


		[DataMember(Name = "status", IsRequired=false)]
		public string Status { get; set; }


		[DataMember(Name = "type", IsRequired=false)]
		public string Type { get; set; }


		[DataMember(Name = "url", IsRequired=false)]
		public string Url { get; set; }


	}
}

