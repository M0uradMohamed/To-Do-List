using ToDoList.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Data
{
    public class ApplicationDbContext : DbContext
    {
   
        public DbSet<Member> Members { get; set; }
        public DbSet<TaskToDo> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true).Build();

            var connection = builder.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connection);
        }
    }
}
