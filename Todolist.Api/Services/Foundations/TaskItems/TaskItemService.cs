//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Todolist.Api.Brokers.Loggings;
using Todolist.Api.Brokers.Storages;
using Todolist.Api.Models.Foundations.TaskItems;
using Todolist.Api.Models.Foundations.TaskItems.Exceptions;

namespace Todolist.Api.Services.Foundations.TaskItems
{
    public class TaskItemService : ITaskItemService
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

        public async ValueTask<TaskItem> AddTaskItemAsync(TaskItem taskitem)
        {
            try
            {
                if (taskitem is null)
                {
                    throw new NullTaskItemException();
                }


                return await this.storageBroker.InsertTaskItemAsync(taskitem);
            }
            catch (NullTaskItemException nullTaskItemException)
            {
                var taskItemValidationException= new TaskItemValidationException(nullTaskItemException);
                
                this.loggingBroker.LogError(taskItemValidationException);
                
                throw taskItemValidationException;
            }
        }
    }
}
