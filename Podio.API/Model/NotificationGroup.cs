using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class NotificationGroup 
	{


        //[DataMember(Name = "context", IsRequired=false)]
        //public  Context { get; set; }


		[DataMember(Name = "notifications", IsRequired=false)]
		public List<Notification> Notifications { get; set; }


	}
}

