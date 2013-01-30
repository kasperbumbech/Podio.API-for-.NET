using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


/// AUTOGENERATED FROM RUBYSOURCE
namespace Podio.API.Model
{
	[DataContract]
	public partial class ApplicationEmail
	{


		[DataMember(Name = "attachments", IsRequired=false)]
		public bool? Attachments { get; set; }


		[DataMember(Name = "mappings", IsRequired=false)]
		public Dictionary<string,object> Mappings { get; set; }


	}
}

