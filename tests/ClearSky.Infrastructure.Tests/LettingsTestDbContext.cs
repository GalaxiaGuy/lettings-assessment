﻿using System.Data.Common;
using ClearSky.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ClearSky.Infrastructure.Tests
{
    public class LettingsTestDbContext : LettingsDbContext
    {
        public LettingsTestDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(CreateInMemoryDatabase(), x => x.MigrationsAssembly("ClearSky.Infrastructure"));
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
        }
    }
}
