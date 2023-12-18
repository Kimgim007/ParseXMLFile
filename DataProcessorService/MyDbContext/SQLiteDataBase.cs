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
            optionsBuilder.UseSqlite("Data Source=E:\\Тестовое задание перезборка\\DataProcessorService\\DataBase.db");
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

