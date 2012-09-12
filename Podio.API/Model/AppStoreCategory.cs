using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class AppStoreCategory 
	{


		[DataMember(Name = "category_id", IsRequired=false)]
		public int CategoryId { get; set; }


		[DataMember(Name = "name", IsRequired=false)]
		public string Name { get; set; }


		[DataMember(Name = "type", IsRequired=false)]
		public string Type { get; set; }


	}
}

