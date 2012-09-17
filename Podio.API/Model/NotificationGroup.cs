using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


/// AUTOGENERATED FROM RUBYSOURCE
namespace Podio.API.Model
{
	[DataContract]
	public class NotificationGroup 
	{


		[DataMember(Name = "context", IsRequired=false)]
		public Podio.API.Utils.SerializableDictionary Context { get; set; }


		[DataMember(Name = "notifications", IsRequired=false)]
		public Podio.API.Utils.SerializableDictionary Notifications { get; set; }


	}
}

