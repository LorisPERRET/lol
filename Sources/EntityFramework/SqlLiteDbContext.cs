using DTO_EF;
using DTO_EF.Mapper;
using Microsoft.EntityFrameworkCore;
using Model;
using StubLib;

namespace EntityFramework
{
    public class SqlLiteDbContext : DbContext
    {

        public StubData _dataManager = new StubData();
        
        public DbSet<ChampionEntity> Champions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source=LolBdd.db");
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