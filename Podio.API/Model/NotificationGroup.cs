using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class NotificationGroup 
	{


		[DataMember(Name = "context", IsRequired=false)]
		public Dictionary<string,string> Context { get; set; }


		[DataMember(Name = "notifications", IsRequired=false)]
		public Dictionary<string,string> Notifications { get; set; }


	}
}

