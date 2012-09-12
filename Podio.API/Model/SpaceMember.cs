using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class SpaceMember 
	{


		[DataMember(Name = "role", IsRequired=false)]
		public string Role { get; set; }


		[DataMember(Name = "invited_on", IsRequired=false)]
		public string InvitedOn { get; set; }


		[DataMember(Name = "started_on", IsRequired=false)]
		public string StartedOn { get; set; }


		[DataMember(Name = "ended_on", IsRequired=false)]
		public string EndedOn { get; set; }


		[DataMember(Name = "user", IsRequired=false)]
		public User User { get; set; }


		[DataMember(Name = "contact", IsRequired=false)]
		public Contact Contact { get; set; }


		[DataMember(Name = "space", IsRequired=false)]
		public Space Space { get; set; }


	}
}

