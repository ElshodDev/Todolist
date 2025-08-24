using Todolist.Api.Models.Foundations.TaskItems;

namespace Todolist.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<TaskItem> InsertTaskItemAsync(TaskItem taskItem);
    }
}
