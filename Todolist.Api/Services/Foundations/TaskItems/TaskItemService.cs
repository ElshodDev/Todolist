//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Todolist.Api.Brokers.Loggings;
using Todolist.Api.Brokers.Storages;
using Todolist.Api.Models.Foundations.TaskItems;

namespace Todolist.Api.Services.Foundations.TaskItems
{
    public partial class TaskItemService : ITaskItemService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public TaskItemService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<TaskItem> AddTaskItemAsync(TaskItem taskitem) =>
            TryCatch(async () =>
            {
                ValidateTaskItemOnAdd(taskitem);

                return await this.storageBroker.InsertTaskItemAsync(taskitem);

            });

        public IQueryable<TaskItem> RetrieveAllTaskItems() =>
          this.storageBroker.SelectAllTaskItems();

        public ValueTask<TaskItem> RetrieveTaskItemByIdAsync(Guid taskItemId) =>
           TryCatch(async () =>
           {
               ValidationTaskItemId(taskItemId);
               var maybeTaskItem =
               await this.storageBroker.SelectTaskItemByIdAsync(taskItemId);

               ValidateStorageTaskItem(maybeTaskItem, taskItemId);

               return maybeTaskItem;
           });
    }
}
