//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Xeptions;

namespace Todolist.Api.Models.Foundations.TaskItems.Exceptions
{
    public class TaskItemDependencyValidationException : Xeption
    {
        public TaskItemDependencyValidationException(Xeption innerException)
         : base(message: "TaskItem dependency validation error occured, fix the errors and try again",
              innerException)
        { }

    }
}
