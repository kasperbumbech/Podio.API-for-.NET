using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class Contact 
	{


		[DataMember(Name = "user_id", IsRequired=false)]
		public int UserId { get; set; }


		[DataMember(Name = "organization", IsRequired=false)]
		public string Organization { get; set; }


		/// <summary>
		///  # Only available when getting contacts for a spac
		/// </summary>
		[DataMember(Name = "role", IsRequired=false)]
		public string Role { get; set; }


		/// <summary>
		///  # Only available when getting contacts for a spac
		/// </summary>
		[DataMember(Name = "removable", IsRequired=false)]
		public bool Removable { get; set; }


		/// <summary>
		///  # user, space, connection - blank probably means it's a real user / Podio membe
		/// </summary>
		[DataMember(Name = "type", IsRequired=false)]
		public string Type { get; set; }


		[DataMember(Name = "space_id", IsRequired=false)]
		public int SpaceId { get; set; }


		[DataMember(Name = "link", IsRequired=false)]
		public string Link { get; set; }


		[DataMember(Name = "last_seen_on", IsRequired=false)]
		public DateTime LastSeenOn { get; set; }


		[DataMember(Name = "rights", IsRequired=false)]
		public string[] Rights { get; set; }


		[DataMember(Name = "external_id", IsRequired=false)]
		public string ExternalId { get; set; }


		[DataMember(Name = "external_picture", IsRequired=false)]
		public string ExternalPicture { get; set; }


	}
}

