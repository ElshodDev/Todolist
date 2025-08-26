//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Microsoft.Data.SqlClient;
using Xeptions;

namespace Todolist.Api.Models.Foundations.TaskItems.Exceptions
{
    public class FailedTaskItemStorageException : Xeption
    {
        public FailedTaskItemStorageException(Xeption innerException)
        : base(message: "Failed taskItem storage error occured, contact support",
              innerException)
        { }

        public FailedTaskItemStorageException(SqlException sqlException)
        {
            SqlException = sqlException;
        }

        public SqlException SqlException { get; }
    }
}
