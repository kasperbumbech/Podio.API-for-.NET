using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class ExternalFile 
	{


		[DataMember(Name = "external_file_id", IsRequired=false)]
		public string ExternalFileId { get; set; }


		[DataMember(Name = "name", IsRequired=false)]
		public string Name { get; set; }


		[DataMember(Name = "mimetype", IsRequired=false)]
		public string Mimetype { get; set; }


		[DataMember(Name = "created_on", IsRequired=false)]
		public DateTime CreatedOn { get; set; }


		[DataMember(Name = "updated_on", IsRequired=false)]
		public DateTime UpdatedOn { get; set; }


	}
}

