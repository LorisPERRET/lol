using API.Controllers;
using DTO_API.Pagination;
using DTO_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using StubLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_API.Mapper;

namespace UnitTestAPI
{
    [TestClass]
    public class UnitTestSkin
    {
        [TestMethod]
        public async Task TestGetSkins()
        {
            IDataManager manager = new StubData();
            SkinController controller = new SkinController(manager, new NullLogger<SkinController>());
            var nbItem = await manager.SkinsMgr.GetNbItems();
            nbItem = nbItem > 10 ? 10 : nbItem;

            var result = await controller.Get() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var skins = result.Value as Page<IEnumerable<SkinDto>>;

            Assert.IsNotNull(skins.Items);
            Assert.AreEqual(nbItem, skins.Items.Count());
        }

        [TestMethod]
        public async Task TestGetSkinsWithPagination()
        {
            IDataManager manager = new StubData();
            SkinController controller = new SkinController(manager, new NullLogger<SkinController>());

            // OK
            var result = await controller.Get(0, 2) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var skins = result.Value as Page<IEnumerable<SkinDto>>;

            Assert.IsNotNull(skins.Items);
            Assert.AreEqual(2, skins.Items.Count());

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
        public async Task TestGetSkinByName()
        {
            IDataManager manager = new StubData();
            SkinController controller = new SkinController(manager, new NullLogger<SkinController>());

            // OK
            var result = await controller.Get("Stinger") as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var skins = result.Value as SkinDto;

            Assert.IsNotNull(skins);
            var skinStub = await manager.SkinsMgr.GetItemsByName("Stinger", 0, await manager.SkinsMgr.GetNbItemsByName("Stinger"));
            Assert.AreEqual(skinStub.Single().Name, skins.Name);

            // NOT OK
            var result2 = await controller.Get("Skin") as NoContentResult;

            Assert.IsNotNull(result2);
            Assert.AreEqual(204, result2.StatusCode);
        }

        [TestMethod]
        public async Task TestGetImage()
        {
            IDataManager manager = new StubData();
            SkinController controller = new SkinController(manager, new NullLogger<SkinController>());

            var skinStub = new Skin("Stinger", new Champion("Akali", ChampionClass.Assassin), 0, "", "image.png");

            var result = await controller.GetImage(skinStub.Name) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var img = result.Value as LargeImageDto;

            Assert.IsNotNull(img);
            Assert.AreEqual(skinStub.Image.Base64, img.Base64);
        }

        [TestMethod]
        public async Task TestPostSkin()
        {
            IDataManager manager = new StubData();
            SkinController controller = new SkinController(manager, new NullLogger<SkinController>());
            var skinDepart = new Skin("Hugo", new Champion("Akali", ChampionClass.Assassin)).ToDto();

            //OK
            var result = await controller.Post(skinDepart) as CreatedAtActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);

            var skinArrive = result.Value as SkinDto;
            Assert.AreEqual(skinDepart.Name, skinArrive.Name);

            //NOT OK
            var result2 = await controller.Post(skinDepart) as BadRequestResult;

            Assert.IsNotNull(result2);
            Assert.AreEqual(400, result2.StatusCode);
        }

        [TestMethod]
        public async Task TestPutSkin()
        {
            IDataManager manager = new StubData();
            SkinController controller = new SkinController(manager, new NullLogger<SkinController>());
            var skinDepart = new Skin("Stinger", new Champion("Akali", ChampionClass.Assassin)).ToDto();

            //OK
            var result = await controller.Put("Stinger", skinDepart) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var skinArrive = result.Value as SkinDto;

            Assert.IsNotNull(skinArrive);
            Assert.AreEqual(skinDepart.Name, skinArrive.Name);

            //NOT OK
            var skinDepart2 = new Skin("Skin", new Champion("Ahri", ChampionClass.Assassin)).ToDto();
            var result2 = await controller.Put("Stinger", skinDepart2) as BadRequestResult;

            Assert.IsNotNull(result2);
            Assert.AreEqual(400, result2.StatusCode);
        }

        [TestMethod]
        public async Task TestDeleteSkin()
        {
            IDataManager manager = new StubData();
            SkinController controller = new SkinController(manager, new NullLogger<SkinController>());

            //OK
            var result = await controller.Delete("Stinger") as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            //NOT OK
            var result2 = await controller.Delete("Skin") as BadRequestResult;

            Assert.IsNotNull(result2);
            Assert.AreEqual(400, result2.StatusCode);
        }
    }
}
