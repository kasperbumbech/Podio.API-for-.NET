using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class ApplicationEmail 
	{


		[DataMember(Name = "attachments", IsRequired=false)]
		public bool Attachments { get; set; }


        //[DataMember(Name = "mappings", IsRequired=false)]
        //public  Mappings { get; set; }


	}
}

