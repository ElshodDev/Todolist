//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Xeptions;

namespace Todolist.Api.Models.Foundations.TaskItems.Exceptions
{
    public class AlreadyExistTaskItemException : Xeption
    {
        public AlreadyExistTaskItemException(Exception innerException)
        : base(message: "TaskItem already exists", innerException) { }

    }
}
