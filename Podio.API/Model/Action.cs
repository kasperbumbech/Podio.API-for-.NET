using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class Action 
	{


		[DataMember(Name = "action_id", IsRequired=false)]
		public int ActionId { get; set; }


		[DataMember(Name = "type", IsRequired=false)]
		public string Type { get; set; }


		[DataMember(Name = "data", IsRequired=false)]
		public Dictionary<string,string> Data { get; set; }


		[DataMember(Name = "text", IsRequired=false)]
		public string Text { get; set; }


	}
}

