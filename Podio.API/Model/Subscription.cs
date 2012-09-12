using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class Subscription 
	{


		[DataMember(Name = "started_on", IsRequired=false)]
		public DateTime StartedOn { get; set; }


		[DataMember(Name = "notifications", IsRequired=false)]
		public int Notifications { get; set; }


		[DataMember(Name = "ref", IsRequired=false)]
		public Dictionary<string,string> Ref { get; set; }


	}
}

