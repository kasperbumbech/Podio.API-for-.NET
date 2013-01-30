using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


/// AUTOGENERATED FROM RUBYSOURCE
namespace Podio.API.Model
{
	[DataContract]
	public partial class Question
	{


		[DataMember(Name = "question_id", IsRequired=false)]
		public int? QuestionId { get; set; }


		[DataMember(Name = "text", IsRequired=false)]
		public string Text { get; set; }


		[DataMember(Name = "ref", IsRequired=false)]
		public Dictionary<string,object> Ref { get; set; }


		[DataMember(Name = "answers", IsRequired=false)]
		public List<QuestionAnswer> Answers { get; set; }


		[DataMember(Name = "options", IsRequired=false)]
		public List<QuestionOption> Options { get; set; }


	}
}

