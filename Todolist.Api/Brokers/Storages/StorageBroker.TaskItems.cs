using Microsoft.EntityFrameworkCore;
using Todolist.Api.Models.Foundations.TaskItems;

namespace Todolist.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
