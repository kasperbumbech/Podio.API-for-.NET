using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Podio.API.Model
{
	[DataContract]
	public class Task 
	{


		[DataMember(Name = "task_id", IsRequired=false)]
		public int? TaskId { get; set; }


		[DataMember(Name = "status", IsRequired=false)]
		public string Status { get; set; }


		[DataMember(Name = "group", IsRequired=false)]
		public string Group { get; set; }


		[DataMember(Name = "text", IsRequired=false)]
		public string Text { get; set; }


		[DataMember(Name = "description", IsRequired=false)]
		public string Description { get; set; }


		[DataMember(Name = "private", IsRequired=false)]
		public bool Private { get; set; }


		[DataMember(Name = "due_date", IsRequired=false)]
		public string DueDate { get; set; }


		[DataMember(Name = "due_time", IsRequired=false)]
		public string DueTime { get; set; }


		/// <summary>
		///  => tru
		/// </summary>
		[DataMember(Name = "due_on, :string", IsRequired=false)]
		public string DueOn { get; set; }


		[DataMember(Name = "responsible", IsRequired=false)]
		public List<User> Responsible { get; set; }


		[DataMember(Name = "space_id", IsRequired=false)]
		public int? SpaceId { get; set; }


		[DataMember(Name = "link", IsRequired=false)]
		public string Link { get; set; }


		[DataMember(Name = "created_on", IsRequired=false)]
		public string CreatedOn { get; set; }


		[DataMember(Name = "completed_on", IsRequired=false)]
		public string CompletedOn { get; set; }


		/// <summary>
		///  # when inputting task
		/// </summary>
		[DataMember(Name = "file_ids", IsRequired=false)]
		public string[] FileIds { get; set; }


		/// <summary>
		///  # when inputting task
		/// </summary>
		[DataMember(Name = "label_ids", IsRequired=false)]
		public string[] LabelIds { get; set; }


		/// <summary>
		///  # when outputting task
		/// </summary>
		[DataMember(Name = "labels", IsRequired=false)]
		public string[] Labels { get; set; }


		[DataMember(Name = "external_id", IsRequired=false)]
		public string ExternalId { get; set; }


		[DataMember(Name = "ref_type", IsRequired=false)]
		public string RefType { get; set; }


		[DataMember(Name = "ref_id", IsRequired=false)]
		public int? RefId { get; set; }


		[DataMember(Name = "ref_title", IsRequired=false)]
		public string RefTitle { get; set; }


		[DataMember(Name = "ref_link", IsRequired=false)]
		public string RefLink { get; set; }


        //[DataMember(Name = "ref", IsRequired=false)]
        //public  Ref { get; set; }


		[DataMember(Name = "created_by", IsRequired=false)]
		public User CreatedBy { get; set; }


		[DataMember(Name = "completed_by", IsRequired=false)]
		public User CompletedBy { get; set; }


		[DataMember(Name = "created_via", IsRequired=false)]
		public Via CreatedVia { get; set; }


		[DataMember(Name = "deleted_via", IsRequired=false)]
		public Via DeletedVia { get; set; }


		[DataMember(Name = "completed_via", IsRequired=false)]
		public Via CompletedVia { get; set; }


		[DataMember(Name = "assignee", IsRequired=false)]
		public User Assignee { get; set; }


		[DataMember(Name = "reminder", IsRequired=false)]
		public Reminder Reminder { get; set; }


		[DataMember(Name = "recurrence", IsRequired=false)]
		public Recurrence Recurrence { get; set; }


		[DataMember(Name = "label_list", IsRequired=false)]
		public List<TaskLabel> LabelList { get; set; }


		[DataMember(Name = "files", IsRequired=false)]
		public List<FileAttachment> Files { get; set; }


		[DataMember(Name = "comments", IsRequired=false)]
		public List<Comment> Comments { get; set; }


	}
}

