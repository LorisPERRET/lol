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
        public DbSet<RunePageEntity> RunePages { get; set; }

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

            // Champion
            modelBuilder.Entity<ChampionEntity>().HasKey(c => c.Name);

            modelBuilder.Entity<ChampionEntity>().Property<int>("ImageId");
            modelBuilder.Entity<ChampionEntity>().HasOne(c => c.Image);

            modelBuilder.Entity<ChampionEntity>().HasMany(c => c.RunePages)
                                                 .WithMany(r => r.Champions);


            // Skin
            modelBuilder.Entity<SkinEntity>().HasKey(s => s.Id);

            modelBuilder.Entity<SkinEntity>().Property<int>("ImageId");
            modelBuilder.Entity<SkinEntity>().HasOne(s => s.Image);

            modelBuilder.Entity<SkinEntity>().Property(s => s.Id)
                                             .ValueGeneratedOnAdd();

            modelBuilder.Entity<SkinEntity>().Property<string>("ChampionName");
            modelBuilder.Entity<SkinEntity>().HasOne(s => s.Champion)
                                             .WithMany(c => c.Skins)
                                             .HasForeignKey("ChampionName");


            // Rune
            modelBuilder.Entity<RuneEntity>().HasKey(r => r.Id);

            modelBuilder.Entity<RuneEntity>().Property<int>("ImageId");
            modelBuilder.Entity<RuneEntity>().HasOne(r => r.Image);

            modelBuilder.Entity<RuneEntity>().Property(r => r.Id)
                                             .ValueGeneratedOnAdd();

            modelBuilder.Entity<RuneEntity>().HasMany(r => r.RunePages)
                                             .WithMany(rp => rp.Runes);


            // RunePage
            modelBuilder.Entity<RunePageEntity>().HasKey(r => r.Id);

            modelBuilder.Entity<RunePageEntity>().Property(r => r.Id)
                                                 .ValueGeneratedOnAdd();

            modelBuilder.Entity<RunePageEntity>().HasMany(rp => rp.Runes)
                                                 .WithMany(r => r.RunePages);
        }
    }
}