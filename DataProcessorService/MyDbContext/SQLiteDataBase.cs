using DataProcessorService.Entitys.EntityForDataBase;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataProcessorService.MyDbContext
{
    public class SQLiteDataBase : DbContext
    {
        public SQLiteDataBase() { }
        public DbSet<ModuleCategoreIDModeleStateEntity> ModuleCategoreIDModeleStates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Укажите свой путь подключения
            //Пример:
            // optionsBuilder.UseSqlite("Data Source=E:\\Тестовое задание\\DataProcessorService\\DataBase.db");
            optionsBuilder.UseSqlite("Data Source=YouPath\\DataProcessorService\\DataBase.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ModuleCategoreIDModeleStateEntity>(entityTypeBuilder =>
            {
                entityTypeBuilder.HasIndex(q => q.ModuleCategoreID).IsUnique();
            });
        }

    }
}

