//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Todolist.Api.Models.Foundations.TaskItems;
using Todolist.Api.Models.Foundations.TaskItems.Exceptions;

namespace Todolist.Api.Tests.Unit.Services.Foundations.TaskItems
{
    public partial class TaskItemServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfTaskItemIsNullAndLogItAsync()
        {
            //given
            TaskItem nullTaskItem = null;
            var nullTaskItemException = new NullTaskItemException();
            var expectedTaskItemValidationException =
                new TaskItemValidationException(nullTaskItemException);

            //when
            ValueTask<TaskItem> addTaskitemTask =
                this.taskItemServic.AddTaskItemAsync(nullTaskItem);

            //then
            await Assert.ThrowsAsync<TaskItemValidationException>(() =>
            addTaskitemTask.AsTask());
        }
    }
}
