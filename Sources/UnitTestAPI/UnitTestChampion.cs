using API.Controllers;
using DTO_API;
using DTO_API.Mapper;
using DTO_API.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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
            ChampionsController controller = new ChampionsController(manager, new NullLogger<ChampionsController>());

            var result = await controller.Get() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var champions = result.Value as Page<IEnumerable<ChampionDto>>;

            Assert.IsNotNull(champions.Items);
            Assert.AreEqual(await manager.ChampionsMgr.GetNbItems(),champions.Items.Count());
        }

        [TestMethod]
        public async Task TestGetChampionsWithPagination()
        {
            IDataManager manager = new StubData();
            ChampionsController controller = new ChampionsController(manager, new NullLogger<ChampionsController>());

            // OK
            var result = await controller.Get(0, 2) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var champions = result.Value as Page<IEnumerable<ChampionDto>>;

            Assert.IsNotNull(champions.Items);
            Assert.AreEqual(2, champions.Items.Count());

            // NOT OK
            // OFFSET NOT OK
            var result2 = await controller.Get(0, 10000) as ForbidResult;
            Assert.IsNotNull(result2);

            var result3 = await controller.Get(0, -1) as ForbidResult;
            Assert.IsNotNull(result3);

            // PAGE NOT OK
            var result4 = await controller.Get(-1, 10) as ForbidResult;
            Assert.IsNotNull(result4);

            var result5 = await controller.Get(1000, 10) as ForbidResult;
            Assert.IsNotNull(result5);
        }

        [TestMethod]
        public async Task TestGetChampionByName()
        {
            IDataManager manager = new StubData();
            ChampionsController controller = new ChampionsController(manager, new NullLogger<ChampionsController>());

            // OK
            var result = await controller.Get("Ahri") as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var champions = result.Value as ChampionDto;

            Assert.IsNotNull(champions);
            var championStub = await manager.ChampionsMgr.GetItemsByName("Ahri", 0, await manager.ChampionsMgr.GetNbItemsByName("Ahri"));
            Assert.AreEqual(championStub.Single().Name, champions.Name);

            // NOT OK
            var result2 = await controller.Get("Hugo") as NoContentResult;

            Assert.IsNotNull(result2);
            Assert.AreEqual(204, result2.StatusCode);
        }

        [TestMethod]
        public async Task TestGetChampionsByClass()
        {
            IDataManager manager = new StubData();
            ChampionsController controller = new ChampionsController(manager, new NullLogger<ChampionsController>());

            // OK
            var result = await controller.Get(0, await manager.ChampionsMgr.GetNbItems(), "Assassin") as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var champions = result.Value as Page<IEnumerable<ChampionDto>>;

            Assert.IsNotNull(champions.Items);
            Assert.AreEqual(await manager.ChampionsMgr.GetNbItemsByClass(ChampionClass.Assassin), champions.Items.Count());

            // NOT OK
            var result2 = await controller.Get(0, await manager.ChampionsMgr.GetNbItems(), "Inexistant") as ForbidResult;
            Assert.IsNotNull(result2);
        }

        [TestMethod]
        public async Task TestGetImage()
        {
            IDataManager manager = new StubData();
            ChampionsController controller = new ChampionsController(manager, new NullLogger<ChampionsController>());

            var championStub = new Champion("Ahri", ChampionClass.Mage, "", "image.png");

            var result = await controller.GetImage(championStub.Name) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var img = result.Value as LargeImageDto;

            Assert.IsNotNull(img);
            Assert.AreEqual(championStub.Image.Base64, img.Base64);
        }

        [TestMethod]
        public async Task TestGetSkins()
        {
            IDataManager manager = new StubData();
            ChampionsController controller = new ChampionsController(manager, new NullLogger<ChampionsController>());

            var championStub = new Champion("Ahri");

            var result = await controller.GetSkins(championStub.Name) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var skins = result.Value as Page<IEnumerable<SkinDto>>;

            Assert.IsNotNull(skins.Items);
            Assert.AreEqual(await manager.SkinsMgr.GetNbItemsByChampion(championStub), skins.Items.Count());
        }

        [TestMethod]
        public async Task TestPostChampion()
        {
            IDataManager manager = new StubData();
            ChampionsController controller = new ChampionsController(manager, new NullLogger<ChampionsController>());
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
            ChampionsController controller = new ChampionsController(manager, new NullLogger<ChampionsController>());
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
            ChampionsController controller = new ChampionsController(manager, new NullLogger<ChampionsController>());

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