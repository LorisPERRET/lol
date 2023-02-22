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
            IDataManager manager = new StubData();
            ChampionsController controller = new ChampionsController(manager);
            
            var result = await controller.Get() as OkObjectResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var champions = result.Value as IEnumerable<ChampionDto>;

            Assert.IsNotNull(champions);
            Assert.AreEqual(await manager.ChampionsMgr.GetNbItems(),champions.Count());

        }

        [TestMethod]
        public async Task TestGetChampion()
        {
            IDataManager manager = new StubData();
            ChampionsController controller = new ChampionsController(manager);

            // OK
            var result = await controller.Get("Ahri") as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var champions = result.Value as ChampionDto;

            Assert.IsNotNull(champions);
            var championStub =
                await manager.ChampionsMgr.GetItemsByName("Ahri", 0, await manager.ChampionsMgr.GetNbItemsByName("Ahri"));
            Assert.AreEqual(championStub.Single().Name, champions.Name);

            // NOT OK
            var result2 = await controller.Get("Hugo") as NoContentResult;

            Assert.IsNotNull(result2);
            Assert.AreEqual(204, result2.StatusCode);

        }

        [TestMethod]
        public async Task TestPostChampion()
        {
            IDataManager manager = new StubData();
            ChampionsController controller = new ChampionsController(manager);
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
            IDataManager manager = new StubData();
            ChampionsController controller = new ChampionsController(manager);
            var championDepart = new Champion("Ahri", ChampionClass.Fighter).ToDto();

            //OK
            var result = await controller.Put("Ahri", championDepart) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var championArrive = result.Value as ChampionDto;

            Assert.IsNotNull(championArrive);
            Assert.AreEqual(championDepart.Name, championArrive.Name);

            //NOT OK
            var championDepart2 = new Champion("Hugo", ChampionClass.Fighter).ToDto();
            var result2 = await controller.Put("Ahri", championDepart2) as BadRequestResult;

            Assert.IsNotNull(result2);
            Assert.AreEqual(400, result2.StatusCode);

        }

        [TestMethod]
        public async Task TestDeleteChampion()
        {
            IDataManager manager = new StubData();
            ChampionsController controller = new ChampionsController(manager);

            //OK
            var result = await controller.Delete("Ahri") as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            //NOT OK
            var result2 = await controller.Delete("Hugo") as BadRequestResult;

            Assert.IsNotNull(result2);
            Assert.AreEqual(400, result2.StatusCode);
        }
    }
}