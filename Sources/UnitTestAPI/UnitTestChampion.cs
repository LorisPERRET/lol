using API.Controllers;
using DTO_API;
using Microsoft.AspNetCore.Mvc;
using StubLib;

namespace UnitTestAPI
{
    [TestClass]
    public class UnitTestChampion
    {
        [TestMethod]
        public async Task TestGetChampions()
        {
            StubData stub = new StubData();
            ChampionsController controller = new ChampionsController(stub);
            
            var result = await controller.Get() as OkObjectResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var champions = result.Value as IEnumerable<ChampionDto>;

            Assert.IsNotNull(champions);
            Assert.AreEqual(await stub.ChampionsMgr.GetNbItems(),champions.Count());

        }

        [TestMethod]
        public async Task TestGetChampion()
        {
            StubData stub = new StubData();
            ChampionsController controller = new ChampionsController(stub);
            
        }

        [TestMethod]
        public async Task TestPostChampion()
        {
            StubData stub = new StubData();
            ChampionsController controller = new ChampionsController(stub);
            
        }

        [TestMethod]
        public async Task TestPutChampion()
        {
            StubData stub = new StubData();
            ChampionsController controller = new ChampionsController(stub);
            
        }

        [TestMethod]
        public async Task TestDeleteChampion()
        {
            StubData stub = new StubData();
            ChampionsController controller = new ChampionsController(stub);
            
        }
    }
}