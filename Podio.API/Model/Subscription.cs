using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class Subscription 
	{


		[DataMember(Name = "started_on", IsRequired=false)]
		public string StartedOn { get; set; }


		[DataMember(Name = "notifications", IsRequired=false)]
		public int? Notifications { get; set; }


        //[DataMember(Name = "ref", IsRequired=false)]
        //public  Ref { get; set; }


	}
}

