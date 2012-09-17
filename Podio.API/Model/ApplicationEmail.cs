using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


/// AUTOGENERATED FROM RUBYSOURCE
namespace Podio.API.Model
{
	[DataContract]
	public class ApplicationEmail 
	{


		[DataMember(Name = "attachments", IsRequired=false)]
		public bool Attachments { get; set; }


		[DataMember(Name = "mappings", IsRequired=false)]
		public Podio.API.Utils.SerializableDictionary Mappings { get; set; }


	}
}

