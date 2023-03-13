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
        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var championAkali = new ChampionEntity { Name = "Akali", Bio = "", Icon = "", Class = ChampionClass.Assassin.ToString(), Image = "" };
            var championAatrox = new ChampionEntity { Name = "Aatrox", Bio = "", Icon = "", Class = ChampionClass.Fighter.ToString(), Image = "" };

            modelBuilder.Entity<ChampionEntity>().HasData(championAkali, championAatrox );

            modelBuilder.Entity<SkinEntity>().HasData(
                new { Id = 1, Name = "Stinger", Description = "", Icon= "", Image = "", ChampionForeignKey = "Akali", Price = 0f },
                new { Id = 2, Name = "Infernal", Description = "", Icon = "", Image = "", ChampionForeignKey = "Akali", Price = 0f },
                new { Id = 3, Name = "All-Star", Description = "", Icon = "", Image = "", ChampionForeignKey = "Akali", Price = 0f },
                new { Id = 4, Name = "Justicar", Description = "", Icon = "", Image = "", ChampionForeignKey = "Aatrox", Price = 0f },
                new { Id = 5, Name = "Mecha", Description = "", Icon = "", Image = "", ChampionForeignKey = "Aatrox", Price = 0f },
                new { Id = 6, Name = "Sea Hunter", Description = "", Icon = "", Image = "", ChampionForeignKey = "Aatrox", Price = 0f }
            );

            modelBuilder.Entity<RuneEntity>().HasData(
                new RuneEntity { Id = 1, Name = "Conqueror", Familly = RuneFamily.Precision.ToString(), Description = "", Image = "" },
                new RuneEntity { Id = 2, Name = "Legend: Alacrity", Familly = RuneFamily.Precision.ToString(), Description = "", Image = "" },
                new RuneEntity { Id = 3, Name = "last stand 2", Familly = RuneFamily.Domination.ToString(), Description = "", Image = "" }
            );
        }
    }
}
