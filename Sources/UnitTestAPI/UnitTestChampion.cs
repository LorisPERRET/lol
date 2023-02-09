using API.Controllers;
using DTO_API;
using DTO_API.Mapper;
using Microsoft.AspNetCore.Mvc;
using Model;
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

            // OK
            var result = await controller.Get("Ahri") as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var champions = result.Value as IEnumerable<ChampionDto>;

            Assert.IsNotNull(champions);
            var championStub =
                await stub.ChampionsMgr.GetItemsByName("Ahri", 0, await stub.ChampionsMgr.GetNbItemsByName("Ahri"));
            Assert.AreEqual(championStub.First().Name, champions.First().Name);

            // NOT OK
            var result2 = await controller.Get("Hugo") as NoContentResult;

            Assert.IsNotNull(result2);
            Assert.AreEqual(204, result2.StatusCode);

        }

        [TestMethod]
        public async Task TestPostChampion()
        {
            StubData stub = new StubData();
            ChampionsController controller = new ChampionsController(stub);
            var championDepart = new Champion("Hugo", ChampionClass.Fighter).ToDto();

            //OK
            var result = await controller.Post(championDepart) as CreatedAtActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);

            var championArrive = result.Value as ChampionDto;
            Assert.AreEqual(championDepart.Name, championArrive.Name);

            //NOT OK
            var result2 = await controller.Post(championDepart) as BadRequestResult;

            Assert.IsNotNull(result2);
            Assert.AreEqual(400, result2.StatusCode);
        }

        [TestMethod]
        public async Task TestPutChampion()
        {
            StubData stub = new StubData();
            ChampionsController controller = new ChampionsController(stub);
            var championDepart = new Champion("Hugo", ChampionClass.Fighter).ToDto();

            var result = await controller.Put("Arhi", championDepart) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task TestDeleteChampion()
        {
            StubData stub = new StubData();
            ChampionsController controller = new ChampionsController(stub);
            
        }
    }
}