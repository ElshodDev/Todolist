//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Todolist.Api.Models.Foundations.TaskItems;

namespace Todolist.Api.Services.Foundations.TaskItems
{
    public interface ITaskItemService
    {
        ValueTask<TaskItem> AddTaskItemAsync(TaskItem taskitem);
        ValueTask<TaskItem> RetrieveTaskItemByIdAsync(Guid taskItemId);
        IQueryable<TaskItem> RetrieveAllTaskItems();
    }
}
