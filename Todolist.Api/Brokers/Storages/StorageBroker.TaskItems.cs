//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Todolist.Api.Models.Foundations.TaskItems;

namespace Todolist.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<TaskItem> TaskItems { get; set; }

        public async ValueTask<TaskItem> InsertTaskItemAsync(TaskItem taskItem)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<TaskItem> taskEntityEnrty =
                 await broker.TaskItems.AddAsync(taskItem);

            await broker.SaveChangesAsync();

            return taskEntityEnrty.Entity;
        }
        public IQueryable<TaskItem> SelectAllTaskItems() =>
     this.TaskItems;

        public async ValueTask<TaskItem> SelectTaskItemByIdAsync(Guid taskItemId) =>
            await this.TaskItems.FindAsync(taskItemId);
    }
}
