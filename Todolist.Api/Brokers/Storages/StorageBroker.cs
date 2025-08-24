//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using EFxceptions;
using Microsoft.EntityFrameworkCore;

namespace Todolist.Api.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private readonly IConfiguration configuration;
        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectingString = this.configuration.GetConnectionString(name: "DefaultConnection");

            optionsBuilder.UseSqlServer(connectingString);
        }

        public override void Dispose() { }

    }
}
