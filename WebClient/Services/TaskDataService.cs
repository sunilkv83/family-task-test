using Core.Extensions.ModelConversion;
using Domain.Commands;
using Domain.Queries;
using Domain.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Abstractions;
using WebClient.Shared.Models;

namespace WebClient.Services
{
    public class TaskDataService: ITaskDataService
    {
        private readonly HttpClient httpClient;

        public TaskDataService(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            Tasks = new List<TaskVm>();
        }

        private IEnumerable<TaskVm> tasks;
        public List<TaskVm> Tasks { get; set; }

        public TaskVm SelectedTask { get; private set; }

        public event EventHandler TasksUpdated;
        public event EventHandler TaskSelected;

        private async Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
        {
            return await httpClient.PostJsonAsync<CreateTaskCommandResult>("task", command);
        }

        private async Task<GetAllTaskQueryResult> GetAllTasks()
        {
            return await httpClient.GetJsonAsync<GetAllTaskQueryResult>("task");
        }


        public void SelectTask(Guid id)
        {
            SelectedTask = Tasks.SingleOrDefault(t => t.Id == id);
            TasksUpdated?.Invoke(this, null);
        }

        public void ToggleTask(Guid id)
        {
            foreach (var taskModel in Tasks)
            {
                if (taskModel.Id == id)
                {
                    taskModel.IsDone = !taskModel.IsDone;
                }
            }

            TasksUpdated?.Invoke(this, null);
        }

        public async void AddTask(TaskVm model)
        {
            //Tasks.Add(model);
            //TasksUpdated?.Invoke(this, null);

            var result = await Create(model.ToCreateTaskCommand());
            if (result != null)
            {
                var updatedList = (await GetAllTasks()).Payload;

                if (updatedList != null)
                {
                    tasks = updatedList;
                    TasksUpdated?.Invoke(this, null);
                    return;
                }
            }
        }
    }
}