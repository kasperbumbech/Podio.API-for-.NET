using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class ContractPrice 
	{


		[DataMember(Name = "total", IsRequired=false)]
		public float Total { get; set; }


		[DataMember(Name = "sub_total", IsRequired=false)]
		public float SubTotal { get; set; }


		[DataMember(Name = "quantity", IsRequired=false)]
		public int? Quantity { get; set; }


		[DataMember(Name = "already_paid", IsRequired=false)]
		public int? AlreadyPaid { get; set; }


		[DataMember(Name = "users", IsRequired=false)]
		public List<User> Users { get; set; }


	}
}

