//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
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
            catch (InvalidTaskItemException invalidTaskItemException)
            {
                throw CreateAndLogValidationException(invalidTaskItemException);
            }
            catch (SqlException sqlException)
            {
                var failedTaskItemStorageException =
               new FailedTaskItemStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedTaskItemStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistTaskItemException =
              new AlreadyExistTaskItemException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistTaskItemException);
            }
        }

        private TaskItemValidationException CreateAndLogValidationException(Xeption exception)
        {
            var taskItemValidationException = new TaskItemValidationException(exception);

            this.loggingBroker.LogError(taskItemValidationException);

            return taskItemValidationException;
        }

        private TaskItemDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var expectedTaskItemDependecyException = new
               TaskItemDependencyException(exception);

            this.loggingBroker.LogCritical(expectedTaskItemDependecyException);

            return expectedTaskItemDependecyException;
        }
        private TaskItemDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {

            var expectedTaskItemDependencyValidationException =
               new TaskItemDependencyValidationException(exception);

            this.loggingBroker.LogError(expectedTaskItemDependencyValidationException);

            return expectedTaskItemDependencyValidationException;
        }

    }
}
