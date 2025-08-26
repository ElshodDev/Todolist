//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Todolist.Api.Models.Foundations.TaskItems;
using Todolist.Api.Models.Foundations.TaskItems.Exceptions;

namespace Todolist.Api.Tests.Unit.Services.Foundations.TaskItems
{
    public partial class TaskItemServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogitAsync()
        {
            //given
            TaskItem someTaskItem = CreateRandomTaskItem();
            SqlException sqlException = GetSqlError();

            var failedTaskItemStorageException =
                new FailedTaskItemStorageException(sqlException);

            var expectedTaskItemDependecyException = new
                TaskItemDependencyException(failedTaskItemStorageException);

            this.storagebrokerMock.Setup(broker =>
           broker.InsertTaskItemAsync(someTaskItem))
               .ThrowsAsync(sqlException);


            //when
            ValueTask<TaskItem> addTaskItemTask =
               this.taskItemService.AddTaskItemAsync(someTaskItem);

            //then
            await Assert.ThrowsAsync<TaskItemDependencyException>(() =>
           addTaskItemTask.AsTask());

            this.storagebrokerMock.Verify(broker =>
            broker.InsertTaskItemAsync(someTaskItem),
            Times.Once);

            this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(
                expectedTaskItemDependecyException))),
                Times.Once);


            this.storagebrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDublicateKeyErrorOccursAndlogitAsync()
        {
            //given
            TaskItem SomeTaskItem = CreateRandomTaskItem();
            string SomeMessage = GetRandomString();

            var duplicateKeyException = new DuplicateKeyException(SomeMessage);

            var alreadyExistTaskItemException =
               new AlreadyExistTaskItemException(duplicateKeyException);

            var expectedTaskItemDependencyValidationException =
                new TaskItemDependencyValidationException(alreadyExistTaskItemException);

            this.storagebrokerMock.Setup(broker =>
           broker.InsertTaskItemAsync(SomeTaskItem))
               .ThrowsAsync(duplicateKeyException);

            //when 
            ValueTask<TaskItem> addTaskItemTask =
               this.taskItemService.AddTaskItemAsync(SomeTaskItem);

            //then
            await Assert.ThrowsAsync<TaskItemDependencyValidationException>(() =>
            addTaskItemTask.AsTask());

            this.storagebrokerMock.Verify(broker =>
            broker.InsertTaskItemAsync(SomeTaskItem),
            Times.Once);


            this.loggingBrokerMock.Verify(Broker =>
            Broker.LogError(It.Is(SameExceptionAs(
                expectedTaskItemDependencyValidationException))),
                Times.Once);

            this.storagebrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
