using DTO_EF;
using DTO_EF.Mapper;
using EntityFramework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Model;
using StubLib;

namespace UnitTestEF
{
    [TestClass]
    public class UnitTestChampion
    {
        [TestMethod]
        public async Task TestAddChampionAsync()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            Champion item = new Champion("Blabla", ChampionClass.Tank);

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();

                //Act
                context.Champions.Add(item.ToEntity());
                context.SaveChanges();

                //Assert
                Assert.AreEqual(context.Champions.Count(), 7 );
                List<ChampionEntity>lesChampions = context.Champions.Where(c => c.Name == item.Name).ToList();
                Assert.AreEqual(lesChampions.FirstOrDefault().Name, item.Name);


            }

        }

        [TestMethod]
        public void TestDeleteChampion()
        {
        }

        [TestMethod]
        public void TestGetChampion()
        {
        }

        [TestMethod]
        public void TestGetChampionByName()
        {
        }

        [TestMethod]
        public void TestGetNbChampion()
        {
            DbDataManager dbManager = new DbDataManager();
            StubData stubData = new StubData();

            var result = dbManager.GetNbItems();

            Assert.IsNotNull(result);
            Assert.AreEqual(stubData.ChampionsMgr.GetNbItems(), result);
        }

        [TestMethod]
        public void TestGetNbChampionByName()
        {
            DbDataManager dbManager = new DbDataManager();
            StubData stubData = new StubData();

            var result = dbManager.GetNbItemsByName("Ahri");

            Assert.IsNotNull(result);
            Assert.AreEqual(stubData.ChampionsMgr.GetNbItemsByName("Ahri"), result);
        }

        [TestMethod]
        public void TestUpdateChampion()
        {
        }
    }
}