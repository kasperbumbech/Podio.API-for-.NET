using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class Recurrence 
	{


		[DataMember(Name = "recurrence_id", IsRequired=false)]
		public int RecurrenceId { get; set; }


		[DataMember(Name = "name", IsRequired=false)]
		public string Name { get; set; }


		[DataMember(Name = "config", IsRequired=false)]
		public Dictionary<string,string> Config { get; set; }


		[DataMember(Name = "step", IsRequired=false)]
		public int Step { get; set; }


		[DataMember(Name = "until", IsRequired=false)]
		public DateTime Until { get; set; }


	}
}

