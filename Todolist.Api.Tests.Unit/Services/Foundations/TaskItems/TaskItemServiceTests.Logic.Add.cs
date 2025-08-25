//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Todolist.Api.Models.Foundations.TaskItems;

namespace Todolist.Api.Tests.Unit.Services.Foundations.TaskItems
{
    public partial class TaskItemServiceTests
    {
        [Fact]
        public async Task ShouldAddTaskItemAsync()
        {
            //given
            TaskItem randomTaskItem = CreateRandomTaskItem();
            TaskItem inputTaskItem = randomTaskItem;
            TaskItem outputTaskItem = inputTaskItem;
            TaskItem expectedTaskItem = outputTaskItem.DeepClone();

            this.storagebrokerMock.Setup(broker =>
               broker.InsertTaskItemAsync(inputTaskItem))
               .ReturnsAsync(outputTaskItem);

            //when 
            TaskItem actualTaskItem =
            await this.taskItemService.AddTaskItemAsync(inputTaskItem);

            //then
            actualTaskItem.Should().BeEquivalentTo(expectedTaskItem);

            this.storagebrokerMock.Verify(broker =>
                broker.InsertTaskItemAsync(inputTaskItem),
                Times.Once);

            this.storagebrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
