using DTO_EF;
using DTO_EF.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model;
using StubLib;

namespace EntityFramework
{
    public class MariaDbContext : DbContext
    {

        
        public DbSet<ChampionEntity> Champions { get; set; }

        public MariaDbContext() { }
        public MariaDbContext(DbContextOptions<MariaDbContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;user=lol;password=ThePassword!;database=lolBdd", new MySqlServerVersion(new Version(10, 11, 1)));
            }
        }

    }
}