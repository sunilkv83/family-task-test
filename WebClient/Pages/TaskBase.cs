using Domain.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Abstractions;

namespace WebClient.Pages
{
     public class TaskBase : ComponentBase
    {
        protected List<MemberVm> members = new List<MemberVm>();
        protected List<MenuItem> leftMenuItem = new List<MenuItem>();

        protected bool showCreator;
        protected bool isLoaded;

        [Inject]
        public IMemberDataService MemberDataService { get; set; }


        [Inject]
        public ITaskDataService TaskDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
          //  UpdateMembers();
            ReloadMenu();

            TaskDataService.TasksUpdated += TaskDataService_TaskChanged;
            showCreator = true;
            isLoaded = true;
        }

        private void TaskDataService_TaskChanged(object sender, EventArgs e)
        {
          //  UpdateMembers();
            ReloadMenu();

            showCreator = true;
            isLoaded = true;

            StateHasChanged();
        }

        //void UpdateMembers()
        //{
        //    var result = MemberDataService.Members;

        //    if (result.Any())
        //    {
        //        members = result.ToList();
        //    }
        //}

        void ReloadMenu()
        {
            for (int i = 0; i < members.Count; i++)
            {
                leftMenuItem.Add(new MenuItem
                {
                    iconColor = members[i].Avatar,
                    label = members[i].FirstName,
                    referenceId = members[i].Id
                });
            }
        }

        //protected void onAddItem()
        //{
        //    showCreator = true;
        //    StateHasChanged();
        //}

        //protected void onTaskAdd(TaskVm taskVm)
        //{
        //    TaskDataService.AddTask(taskVm);
        //}

    }
}
