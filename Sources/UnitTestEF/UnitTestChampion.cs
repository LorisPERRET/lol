using EntityFramework;
using StubLib;

namespace UnitTestEF
{
    [TestClass]
    public class UnitTestChampion
    {
        [TestMethod]
        public void TestAddChampion()
        {
            DbDataManager dbManager = new DbDataManager();

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