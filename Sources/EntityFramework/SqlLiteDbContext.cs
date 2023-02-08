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

        public StubData _dataManager = new StubData();
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

        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Champion> champ = await _dataManager.ChampionsMgr.GetItems(0,
                await _dataManager.ChampionsMgr.GetNbItems());

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ChampionEntity>().HasData(
                champ.ToEntities()
            );
        }
    }
}