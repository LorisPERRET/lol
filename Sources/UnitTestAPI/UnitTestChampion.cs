using API.Controllers;
using DTO_API;
using DTO_API.Mapper;
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

            var result = await controller.Get("Ahri") as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var champions = result.Value as IEnumerable<ChampionDto>;

            Assert.IsNotNull(champions);
            var championStub =
                await stub.ChampionsMgr.GetItemsByName("Ahri", 0, await stub.ChampionsMgr.GetNbItemsByName("Ahri"));
            Assert.AreEqual(championStub.First().Name, champions.First().Name);

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