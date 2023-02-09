using DTO_EF;
using DTO_EF.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model;
using StubLib;

namespace EntityFramework
{
    public class SqlLiteDbContext : DbContext
    {

        
        public DbSet<ChampionEntity> Champions { get; set; }

        public SqlLiteDbContext() { }
        public SqlLiteDbContext(DbContextOptions<SqlLiteDbContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source=LolBdd.db");
            }
        }

    }
}