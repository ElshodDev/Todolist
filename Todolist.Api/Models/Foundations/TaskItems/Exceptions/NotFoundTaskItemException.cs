//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Xeptions;

namespace Todolist.Api.Models.Foundations.TaskItems.Exceptions
{
    public class NotFoundTaskItemException : Xeption
    {
        public NotFoundTaskItemException(Guid taskItemId)
            : base(message: $"TaskItem is not found with id: {taskItemId}.")
        { }
    }
}
