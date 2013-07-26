using Podio.API.Model;
using Podio.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace Podio.API.Services
{
    public class TaskService
    {
        private Client _client;
        /// <summary>
        /// Add a client and you can use this as a shortcut to the Podio REST API 
        /// </summary>
        public TaskService(Client client)
        {
            _client = client;
        }

        /// <summary>
        /// https://developers.podio.com/doc/tasks/get-task-22413
        /// </summary>
        public Task GetTask(int taskId)
        {
            return PodioRestHelper.Request<Task>(Constants.PODIOAPI_BASEURL + "/task/" + taskId, _client.AuthInfo.AccessToken).Data;
        }

        [DataContract]
        public struct CreateUpdateRequest
        {
            [DataMember(Name = "text", IsRequired = false)]
            public string Text { get; set; }

            [DataMember(Name = "description", IsRequired = false)]
            public string Description { get; set; }

            [DataMember(Name = "private", IsRequired = false)]
            public bool? Private { get; set; }

            [DataMember(Name = "due_date", IsRequired = false)]
            public string DueDate { get; set; }

            [DataMember(Name = "due_time", IsRequired = false)]
            public string DueTime { get; set; }

            [DataMember(Name = "due_on", IsRequired = false)]
            public DateTime? DueOn { get; set; }

            [DataMember(Name = "responsible", IsRequired = false)]
            public Dictionary<string, object> Responsible { get; set; }

            [DataMember(Name = "file_ids", IsRequired = false)]
            public List<int> FileIds { get; set; }

            [DataMember(Name = "labels", IsRequired = false)]
            public string[] Labels { get; set; }

            [DataMember(Name = "label_ids", IsRequired = false)]
            public string[] LabelIds { get; set; }

            [DataMember(Name = "reminder", IsRequired = false)]
            public Reminder Reminder { get; set; }

            [DataMember(Name = "recurrence", IsRequired = false)]
            public Recurrence Recurrence { get; set; }

            [DataMember(Name = "external_id", IsRequired = false)]
            public string ExternalId { get; set; }

            [DataMember(Name = "ref_type", IsRequired = false)]
            public string RefType { get; set; }

            [DataMember(Name = "ref_id", IsRequired = false)]
            public int? RefId { get; set; }
        }

        /// <summary>
        /// https://developers.podio.com/doc/tasks/create-task-22419
        /// or
        /// https://developers.podio.com/doc/tasks/create-task-with-reference-22420
        /// </summary>
        public int AddNewTask(Task task, bool silent = false)
        {
            var requestData = new CreateUpdateRequest()
            {
                Text = task.Text,
                Description = task.Description,
                Private = task.Private,
                DueDate = ((DateTime)task.DueDate).ToString("yyyy-MM-dd"),
                DueTime = ((DateTime)task.DueTime).ToString("HH:mm"),
                DueOn = task.DueOn,
                Responsible = task.Responsible,
                FileIds = task.FileIds,
                Labels = task.Labels,
                LabelIds = task.LabelIds,
                Reminder = task.Reminder,
                Recurrence = task.Recurrence,
                ExternalId = task.ExternalId
            };
            var newTask = AddNewTask(requestData, task.RefType, task.RefId, silent);
            task.TaskId = newTask.TaskId;
            return (int)task.TaskId;
        }

        /// <summary>
        /// https://developers.podio.com/doc/tasks/create-task-22419
        /// or
        /// https://developers.podio.com/doc/tasks/create-task-with-reference-22420
        /// </summary>
        public Task AddNewTask(CreateUpdateRequest requestData, string ref_type = null, int? ref_id = null, bool silent = false)
        {
            if (!String.IsNullOrEmpty(ref_type) && ref_id.HasValue)
            {
                return PodioRestHelper.JSONRequest<Task>(Constants.PODIOAPI_BASEURL + "/task/" + ref_type + "/" + ref_id.ToString() + "/",
                                                         _client.AuthInfo.AccessToken,
                                                         requestData,
                                                         PodioRestHelper.RequestMethod.POST).Data;
            }
            else
            {
                return PodioRestHelper.JSONRequest<Task>(Constants.PODIOAPI_BASEURL + "/task/",
                                                         _client.AuthInfo.AccessToken,
                                                         requestData,
                                                         PodioRestHelper.RequestMethod.POST).Data;
            }
        }

        /// <summary>
        /// https://developers.podio.com/doc/tasks/update-task-10583674
        /// </summary>
        public void UpdateTask(Task task)
        {
            var requestData = new CreateUpdateRequest()
            {
                Text = task.Text,
                Description = task.Description,
                DueDate = ((DateTime)task.DueDate).ToString("yyyy-MM-dd"),
                DueTime = ((DateTime)task.DueTime).ToString("HH:mm"),
                Responsible = task.Responsible,
                Private = task.Private,
                RefType = task.RefType,
                RefId = task.RefId,
                Labels = task.Labels,
                FileIds = task.FileIds
            };
            UpdateTask((int)task.TaskId, requestData);
        }

        /// <summary>
        /// https://developers.podio.com/doc/tasks/update-task-10583674
        /// </summary>
        public void UpdateTask(int taskId, CreateUpdateRequest requestData)
        {
            PodioRestHelper.JSONRequest<Task>(Constants.PODIOAPI_BASEURL + "/task/" + taskId,
                                              _client.AuthInfo.AccessToken,
                                              requestData,
                                              PodioRestHelper.RequestMethod.PUT);
        }

        /// <summary>
        /// https://developers.podio.com/doc/tasks/update-task-text-22428
        /// </summary>
        public void UpdateTaskText(int taskId, string text)
        {
            var rq = new CreateUpdateRequest();
            rq.Text = text;
            PodioRestHelper.JSONRequest<Task>(Constants.PODIOAPI_BASEURL + "/task/" + taskId + "/text",
                                              _client.AuthInfo.AccessToken,
                                              rq,
                                              PodioRestHelper.RequestMethod.PUT);
        }

        /// <summary>
        /// https://developers.podio.com/doc/tasks/update-task-description-76982
        /// </summary>
        public void UpdateTaskDescription(int taskId, string description)
        {
            var rq = new CreateUpdateRequest();
            rq.Description = description;
            PodioRestHelper.JSONRequest<Task>(Constants.PODIOAPI_BASEURL + "/task/" + taskId + "/description",
                                              _client.AuthInfo.AccessToken,
                                              rq,
                                              PodioRestHelper.RequestMethod.PUT);
        }

        /// <summary>
        /// https://developers.podio.com/doc/tasks/update-task-due-on-3442633
        /// </summary>
        public void UpdateTaskDue(int taskId, DateTime date, bool dateOnly = false)
        {
            var rq = new CreateUpdateRequest();
            rq.DueDate = date.ToString("yyyy-MM-dd");
            if (!dateOnly) rq.DueTime = date.ToString("HH:mm");
            PodioRestHelper.JSONRequest<Task>(Constants.PODIOAPI_BASEURL + "/task/" + taskId + "/due",
                                              _client.AuthInfo.AccessToken,
                                              rq,
                                              PodioRestHelper.RequestMethod.PUT);
        }

        /// <summary>
        /// https://developers.podio.com/doc/tasks/complete-task-22432
        /// </summary>
        public PodioRestHelper.PodioResponse CompleteTask(int taskId)
        {
            return PodioRestHelper.Request(Constants.PODIOAPI_BASEURL + "/task/" + taskId + "/complete",
                                           _client.AuthInfo.AccessToken,
                                           null,
                                           PodioRestHelper.RequestMethod.POST);
        }

        /// <summary>
        /// https://developers.podio.com/doc/tasks/delete-task-77179
        /// </summary>
        public PodioRestHelper.PodioResponse DeleteTasks(IEnumerable<int> taskIds)
        {
            return PodioRestHelper.JSONRequest(Constants.PODIOAPI_BASEURL + "/task/" + String.Join(",", taskIds.Select(id => id.ToString()).ToArray()),
                                               _client.AuthInfo.AccessToken,
                                               null,
                                               PodioRestHelper.RequestMethod.DELETE);
        }

        /// <summary>
        /// https://developers.podio.com/doc/tasks/delete-task-77179
        /// </summary>
        public PodioRestHelper.PodioResponse DeleteTask(int taskId)
        {
            return DeleteTasks(new int[] { taskId });
        }
    }
}
