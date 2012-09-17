using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


/// AUTOGENERATED FROM RUBYSOURCE
namespace Podio.API.Model
{
	[DataContract]
	public class OrganizationProfile 
	{


		[DataMember(Name = "org_id", IsRequired=false)]
		public int? OrgId { get; set; }


		[DataMember(Name = "avatar", IsRequired=false)]
		public int? Avatar { get; set; }


		[DataMember(Name = "image", IsRequired=false)]
		public Podio.API.Utils.SerializableDictionary Image { get; set; }


		[DataMember(Name = "name", IsRequired=false)]
		public string Name { get; set; }


		[DataMember(Name = "mail", IsRequired=false)]
		public string[] Mail { get; set; }


		[DataMember(Name = "phone", IsRequired=false)]
		public string[] Phone { get; set; }


		[DataMember(Name = "url", IsRequired=false)]
		public string[] Url { get; set; }


		[DataMember(Name = "address", IsRequired=false)]
		public string[] Address { get; set; }


		[DataMember(Name = "city", IsRequired=false)]
		public string City { get; set; }


		[DataMember(Name = "country", IsRequired=false)]
		public string Country { get; set; }


		[DataMember(Name = "about", IsRequired=false)]
		public string About { get; set; }


	}
}

