//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Xeptions;

namespace Todolist.Api.Models.Foundations.TaskItems.Exceptions
{
    public class NullTaskItemException : Xeption
    {
        public NullTaskItemException()
            :base(message: "TaskItem is null")
        { }
    }
}
