using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class OrganizationContact 
	{


		[DataMember(Name = "org_id", IsRequired=false)]
		public int? OrgId { get; set; }


		/// <summary>
		///  # The name of the primary organization contac
		/// </summary>
		[DataMember(Name = "attention", IsRequired=false)]
		public string Attention { get; set; }


	}
}

