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
        private void ValidateTaskItemOnAdd(TaskItem taskItem)
        {
            ValidateTaskItemNotNull(taskItem);

            Validate(
              (Rule: IsInvalid(taskItem.Id), Parameter: nameof(TaskItem.Id)),
              (Rule: IsInvalid(taskItem.Title), Parameter: nameof(TaskItem.Title)),
              (Rule: IsInvalid(taskItem.CreatedAt), Parameter: nameof(TaskItem.CreatedAt))
            );
        }

        private void ValidateTaskItemNotNull(TaskItem taskItem)
        {
            if (taskItem is null)
            {
                throw new NullTaskItemException();
            }
        }
        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "id is required"
        };
        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "text is required"
        };

        private static dynamic IsInvalid(DateTime date) => new
        {
            Condition = date == default,
            Message = "date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidTaskItemException = new InvalidTaskItemException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidTaskItemException.UpsertDataList(
                        key: parameter,
                       value: rule.Message);
                }
            }

            invalidTaskItemException.ThrowIfContainsErrors();
        }

    }
}
