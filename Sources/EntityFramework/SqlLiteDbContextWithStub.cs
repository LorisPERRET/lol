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

namespace EntityFramework
{
    public class SqlLiteDbContextWithStub : SqlLiteDbContext
    {
        public StubData _dataManager = new StubData();
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
