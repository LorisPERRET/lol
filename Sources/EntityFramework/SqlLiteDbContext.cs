using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class SqlLiteDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source=LolBdd.db");
        }
    }
}