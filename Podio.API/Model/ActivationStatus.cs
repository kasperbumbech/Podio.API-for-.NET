using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class ActivationStatus 
	{


		[DataMember(Name = "space_count", IsRequired=false)]
		public int SpaceCount { get; set; }


	}
}

