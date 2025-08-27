//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Todolist.Api.Models.Foundations.TaskItems;

namespace Todolist.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<TaskItem> InsertTaskItemAsync(TaskItem taskItem);
        IQueryable<TaskItem> SelectAllTaskItems();
        ValueTask<TaskItem> SelectTaskItemByIdAsync(Guid taskItemId);
    }
}
