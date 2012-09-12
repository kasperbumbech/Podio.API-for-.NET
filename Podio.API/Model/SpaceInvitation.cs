using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class SpaceInvitation 
	{


		[DataMember(Name = "space_id", IsRequired=false)]
		public int SpaceId { get; set; }


		[DataMember(Name = "role", IsRequired=false)]
		public string Role { get; set; }


		[DataMember(Name = "subject", IsRequired=false)]
		public string Subject { get; set; }


		[DataMember(Name = "message", IsRequired=false)]
		public string Message { get; set; }


		[DataMember(Name = "notify", IsRequired=false)]
		public bool Notify { get; set; }


		[DataMember(Name = "users", IsRequired=false)]
		public string[] Users { get; set; }


		[DataMember(Name = "mails", IsRequired=false)]
		public string[] Mails { get; set; }


		[DataMember(Name = "profiles", IsRequired=false)]
		public string[] Profiles { get; set; }


		[DataMember(Name = "activation_code", IsRequired=false)]
		public int ActivationCode { get; set; }


		/// <summary>
		///  # Writ
		/// </summary>
		[DataMember(Name = "context_ref_type", IsRequired=false)]
		public string ContextRefType { get; set; }


		/// <summary>
		///  # Writ
		/// </summary>
		[DataMember(Name = "context_ref_id", IsRequired=false)]
		public int ContextRefId { get; set; }


		/// <summary>
		///  # Rea
		/// </summary>
		[DataMember(Name = "context", IsRequired=false)]
		public Dictionary<string,string> Context { get; set; }


		[DataMember(Name = "external_contacts", IsRequired=false)]
		public Dictionary<string,string> ExternalContacts { get; set; }


		[DataMember(Name = "user", IsRequired=false)]
		public User User { get; set; }


	}
}

