using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class Reminder 
	{


		[DataMember(Name = "reminder_id", IsRequired=false)]
		public int ReminderId { get; set; }


		[DataMember(Name = "remind_delta", IsRequired=false)]
		public int RemindDelta { get; set; }


	}
}

