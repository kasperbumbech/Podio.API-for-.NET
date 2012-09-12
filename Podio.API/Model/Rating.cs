using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class Rating 
	{


		[DataMember(Name = "rating_id", IsRequired=false)]
		public int? RatingId { get; set; }


		[DataMember(Name = "type", IsRequired=false)]
		public string Type { get; set; }


		[DataMember(Name = "value", IsRequired=false)]
		public string Value { get; set; }


	}
}

