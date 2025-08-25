//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Todolist.Api.Models.Foundations.TaskItems;
using Todolist.Api.Models.Foundations.TaskItems.Exceptions;
using Xeptions;

namespace Todolist.Api.Services.Foundations.TaskItems
{
    public partial class TaskItemService
    {
        private delegate ValueTask<TaskItem> ReturningTaskItemFunction();

        private async ValueTask<TaskItem> TryCatch(ReturningTaskItemFunction returningTaskItemFunction)
        {
            try
            {
                return await returningTaskItemFunction();
            }
            catch (NullTaskItemException nullTaskItemException)
            {
                throw CreateAndLogValidationException(nullTaskItemException);
            }
        }
        private TaskItemValidationException CreateAndLogValidationException(Xeption exception)
        {
            var taskItemValidationException = new TaskItemValidationException(exception);

            this.loggingBroker.LogError(taskItemValidationException);

            return taskItemValidationException;
        }
    }
}
