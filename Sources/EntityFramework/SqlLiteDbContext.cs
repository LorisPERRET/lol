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
        public DbSet<SkinEntity> Skins { get; set; }
        public DbSet<RuneEntity> Runes { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChampionEntity>().HasKey(c => c.Name);

            modelBuilder.Entity<SkinEntity>().HasKey(s => s.Id);
            modelBuilder.Entity<SkinEntity>().Property(s => s.Id)
                                             .ValueGeneratedOnAdd();
            modelBuilder.Entity<SkinEntity>().Property<string>("ChampionForeignKey");
            modelBuilder.Entity<SkinEntity>().HasOne(s => s.Champion)
                                             .WithMany(c => c.Skins)
                                             .HasForeignKey("ChampionForeignKey");

            modelBuilder.Entity<RuneEntity>().HasKey(r => r.Id);
            modelBuilder.Entity<RuneEntity>().Property(r => r.Id)
                                             .ValueGeneratedOnAdd();

        }

    }
}