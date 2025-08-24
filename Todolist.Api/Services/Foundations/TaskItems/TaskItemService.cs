//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Todolist.Api.Brokers.Storages;
using Todolist.Api.Models.Foundations.TaskItems;

namespace Todolist.Api.Services.Foundations.TaskItems
{
    public class TaskItemService : ITaskItemService
    {
        private readonly IStorageBroker storageBroker;

        public TaskItemService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;


        public async ValueTask<TaskItem> AddTaskItemAsync(TaskItem taskitem) =>
            await this.storageBroker.InsertTaskItemAsync(taskitem);
    }
}
