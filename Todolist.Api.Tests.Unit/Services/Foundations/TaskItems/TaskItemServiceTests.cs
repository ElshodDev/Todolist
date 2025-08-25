//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Moq;
using Todolist.Api.Brokers.Loggings;
using Todolist.Api.Brokers.Storages;
using Todolist.Api.Models.Foundations.TaskItems;
using Todolist.Api.Services.Foundations.TaskItems;
using Tynamix.ObjectFiller;

namespace Todolist.Api.Tests.Unit.Services.Foundations.TaskItems
{
    public partial class TaskItemServiceTests
    {
        private readonly Mock<IStorageBroker> storagebrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ITaskItemService taskItemService;

        public TaskItemServiceTests()
        {
            this.storagebrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.taskItemService =
              new TaskItemService(
              this.storagebrokerMock.Object,
              this.loggingBrokerMock.Object);
        }

        private static TaskItem CreateRandomTaskItem() =>
      CreateTaskItemFiller().Create();

        private static Filler<TaskItem> CreateTaskItemFiller()
        {
            var filler = new Filler<TaskItem>();

            filler.Setup()
                .OnType<Guid>().Use(Guid.NewGuid)
                .OnProperty(t => t.CreatedAt).Use(DateTime.UtcNow)
                .OnProperty(t => t.IsCompleted).Use(GetRandomBool);

            return filler;
        }

        private static bool GetRandomBool()
        {
            return new Random().Next(0, 2) == 1;
        }
    }
}
