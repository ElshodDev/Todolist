//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Todolist.Api.Models.Foundations.TaskItems;
using Todolist.Api.Models.Foundations.TaskItems.Exceptions;

namespace Todolist.Api.Services.Foundations.TaskItems
{
    public partial class TaskItemService
    {
        private void ValidateTaskItemNotNull(TaskItem taskItem)
        {
            if (taskItem == null)
            {
                throw new NullTaskItemException();
            }
        }
    }
}
