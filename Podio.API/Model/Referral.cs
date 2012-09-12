using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class Referral 
	{


		[DataMember(Name = "code", IsRequired=false)]
		public string Code { get; set; }


		[DataMember(Name = "status", IsRequired=false)]
		public string Status { get; set; }


	}
}

