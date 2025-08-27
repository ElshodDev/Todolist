//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Moq;
using System.Linq.Expressions;
using Todolist.Api.Models.Foundations.TaskItems;
using Todolist.Api.Models.Foundations.TaskItems.Exceptions;
using Xeptions;

namespace Todolist.Api.Tests.Unit.Services.Foundations.TaskItems
{
    public partial class TaskItemServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfTaskItemIsNullAndLogItAsync()
        {
            // given
            TaskItem nullTaskItem = null;

            var nullTaskItemException = new NullTaskItemException();

            var expectedTaskItemValidationException =
                new TaskItemValidationException(nullTaskItemException);

            // when
            ValueTask<TaskItem> addTaskItemTask =
                this.taskItemService.AddTaskItemAsync(nullTaskItem);

            // then
            await Assert.ThrowsAsync<TaskItemValidationException>(() =>
                addTaskItemTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTaskItemValidationException))),
                Times.Once);

            this.storagebrokerMock.Verify(broker =>
            broker.InsertTaskItemAsync(It.IsAny<TaskItem>()),
            Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storagebrokerMock.VerifyNoOtherCalls();
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfTaskItemIsinvalidAndlogitAsync(
            string invalidText)
        {
            //given 
            var invalidTaskItem = new TaskItem
            {
                Id = Guid.Empty,
                Title = invalidText,
                CreatedAt = default
            };

            var invalidTaskItemException = new InvalidTaskItemException();

            invalidTaskItemException.AddData(
                nameof(TaskItem.Id),
                values: "id is required");

            invalidTaskItemException.AddData(
              nameof(TaskItem.Title),
              values: "text is required");

            invalidTaskItemException.AddData(
                nameof(TaskItem.CreatedAt),
                values: "date is required");

            var expectedTaskItemValidationException =
            new TaskItemValidationException(invalidTaskItemException);

            //when
            ValueTask<TaskItem> addTaskItemTask =
            this.taskItemService.AddTaskItemAsync(invalidTaskItem);

            //then
            await Assert.ThrowsAsync<TaskItemValidationException>(() =>
            addTaskItemTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedTaskItemValidationException))),
                Times.Once());

            this.storagebrokerMock.Verify(Broker =>
            Broker.InsertTaskItemAsync(It.IsAny<TaskItem>()),
            Times.Never());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storagebrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAsync()
        {
            //given 
            Guid invalidId = Guid.Empty;

            var invalidTaskItemException = new InvalidTaskItemException();

            invalidTaskItemException.AddData(
             nameof(TaskItem.Id),
              "id is required");

            var expectedTaskItemValidationException =
              new TaskItemValidationException(invalidTaskItemException);

            // when
            ValueTask<TaskItem> retrieveTaskItemByIdTask =
                this.taskItemService.RetrieveTaskItemByIdAsync(invalidId);

            //then
            await Assert.ThrowsAsync<TaskItemValidationException>(() =>
        retrieveTaskItemByIdTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTaskItemValidationException))),
                Times.Once);

            this.storagebrokerMock.Verify(broker =>
                broker.SelectTaskItemByIdAsync(It.IsAny<Guid>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storagebrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnUpdateIfTaskItemIsNullAsync()
        {
            // given
            TaskItem nullTaskItem = null;
            var invalidTaskItemException = new InvalidTaskItemException();
            var expectedTaskItemValidationException =
                new TaskItemValidationException(invalidTaskItemException);

            // when
            ValueTask<TaskItem>  modifyTaskItemTask =
                this.taskItemService.ModifyTaskItemAsync(nullTaskItem);

            // then
            await Assert.ThrowsAsync<TaskItemValidationException>(() =>
                modifyTaskItemTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedTaskItemValidationException))),
                Times.Once);

            this.storagebrokerMock.Verify(broker  =>
                broker.UpdateAsync(It.IsAny<TaskItem>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storagebrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnDeleteIfTaskItemIsNullAsync()
        {
            // given
            TaskItem nullTaskItem = null;

            var nullTaskItemException = new NullTaskItemException();

            var expectedTaskItemValidationException =
                new TaskItemValidationException(nullTaskItemException);

            // when
            ValueTask<TaskItem> DeleteTaskItemTask =
                this.taskItemService.DeleteTaskItemAsync(nullTaskItem);

            // then
            await Assert.ThrowsAsync<TaskItemValidationException>(() =>
                DeleteTaskItemTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedTaskItemValidationException))),
                Times.Once);

            this.storagebrokerMock.Verify(broker =>
                broker.DeleteAsync(It.IsAny<TaskItem>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storagebrokerMock.VerifyNoOtherCalls();

        }

        private Expression<Func<Xeption, bool>> SameExceptionAs(
            Xeption expectedTaskItemValidationException) =>
            actualtaskItem =>
            actualtaskItem.SameExceptionAs(expectedTaskItemValidationException);


    }
}
