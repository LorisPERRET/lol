using DTO_EF;
using DTO_EF.Mapper;
using Microsoft.EntityFrameworkCore;
using Model;
using StubLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rune = Model.Rune;

namespace EntityFramework
{
    public class SqlLiteDbContextWithStub : SqlLiteDbContext
    {
        public StubData _dataManager = new StubData();
        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChampionEntity>().HasData(
                new ChampionEntity { Name = "Akali", Bio = "", Icon = "", Class = ChampionClass.Assassin.ToString(), Image = "" },
                new ChampionEntity { Name = "Aatrox", Bio = "", Icon = "", Class = ChampionClass.Fighter.ToString(), Image = "" }
            );
            modelBuilder.Entity<SkinEntity>().HasData(
                new SkinEntity("Stinger", "Akali"),
                new SkinEntity("Infernal", "Akali"),
                new SkinEntity("All-Star", "Akali"),
                new SkinEntity("Justicar", "Aatrox"),
                new SkinEntity("Mecha", "Aatrox"),
                new SkinEntity("Sea Hunter", "Aatrox"),
                new SkinEntity("Dynasty", "Ahri"),
                new SkinEntity("Midnight", "Ahri"),
                new SkinEntity("Foxfire", "Ahri"),
            );
            modelBuilder.Entity<RuneEntity>().HasData(
                new RuneEntity("Conqueror", RuneFamily.Precision.ToString()),
                new RuneEntity("Triumph", RuneFamily.Precision.ToString()),
                new RuneEntity("Legend: Alacrity", RuneFamily.Precision.ToString()),
                new RuneEntity("Legend: Tenacity", RuneFamily.Precision.ToString()),
                new RuneEntity("last stand", RuneFamily.Domination.ToString()),
                new RuneEntity("last stand 2", RuneFamily.Domination.ToString())
            );
        }
    }
}
