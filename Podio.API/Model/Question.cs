using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class Question 
	{


		[DataMember(Name = "question_id", IsRequired=false)]
		public int? QuestionId { get; set; }


		[DataMember(Name = "text", IsRequired=false)]
		public string Text { get; set; }


        //[DataMember(Name = "ref", IsRequired=false)]
        //public object Ref { get; set; }


		[DataMember(Name = "answers", IsRequired=false)]
		public List<QuestionAnswer> Answers { get; set; }


		[DataMember(Name = "options", IsRequired=false)]
		public List<QuestionOption> Options { get; set; }


	}
}

