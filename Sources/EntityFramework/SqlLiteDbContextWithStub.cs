using DTO_EF;
using DTO_EF.Mapper;
using Microsoft.EntityFrameworkCore;
using Model;
using StubLib;

namespace EntityFramework
{
    public class SqlLiteDbContextWithStub : SqlLiteDbContext
    {
        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var stub = new StubData();
            var championList = await stub.ChampionsMgr.GetItems(0, await stub.ChampionsMgr.GetNbItems());
            int imageId = 1;
            int id = 1;

            foreach (var champion in championList.ToList())
            {
                var image = new ImageEntity { Id = imageId, base64 = champion.Image.Base64 };
                modelBuilder.Entity<ImageEntity>().HasData(image);
                modelBuilder.Entity<ChampionEntity>().HasData(
                    new { champion.Name, champion.Bio, champion.Icon, Class = champion.Class.ToString(), ImageId = image.Id }
                );
                imageId++;
            }


            var skinList = await stub.SkinsMgr.GetItems(0, await stub.SkinsMgr.GetNbItems());

            foreach (var skin in skinList.ToList())
            {
                var image = new ImageEntity { Id = imageId, base64 = skin.Image.Base64 };
                modelBuilder.Entity<ImageEntity>().HasData(image);
                modelBuilder.Entity<SkinEntity>().HasData(
                    new { Id = id, skin.Name, skin.Description, skin.Icon, ImageId = image.Id, ChampionName = skin.Champion.Name, skin.Price }
                );
                imageId++;
                id++;
            }

            var runeList = await stub.RunesMgr.GetItems(0, await stub.SkinsMgr.GetNbItems());
            id = 1;
            foreach (var rune in runeList.ToList())
            {
                var image = new ImageEntity { Id = imageId, base64 = rune.Image.Base64 };
                modelBuilder.Entity<ImageEntity>().HasData(image);
                modelBuilder.Entity<RuneEntity>().HasData(
                    new { Id = id, rune.Name, Familly = rune.Family.ToString(), rune.Description, ImageId = image.Id }
                );
                imageId++;
                id++;
            }

            var runePageList = await stub.RunePagesMgr.GetItems(0, await stub.RunePagesMgr.GetNbItems());
            id = 1;

            foreach (var runePage in runePageList.ToList())
            {
                modelBuilder.Entity<RunePageEntity>().HasData(
                    new { Id = id, runePage.Name }
                );
                id++;
            }
        }
    }
}
