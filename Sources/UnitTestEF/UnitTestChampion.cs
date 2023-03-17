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
        public async Task TestAddChampion()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            ChampionEntity item = new ChampionEntity { Name = "Blabla", Bio = "", Class = "Tank", Icon = "", Image = new ImageEntity { base64 = "" } };

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();

                //Act
                await context.Champions.AddAsync(item);
                context.SaveChanges();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(context.Champions.Count(), 1);
                List<ChampionEntity> lesChampions = context.Champions.Where(c => c.Name == item.Name).ToList();
                Assert.AreEqual(lesChampions.FirstOrDefault().Name, item.Name);
            }
        }

        [TestMethod]
        public async Task TestDeleteChampion()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            ChampionEntity item = new ChampionEntity { Name = "Blabla", Bio = "", Class = "Tank", Icon = "", Image = new ImageEntity { base64 = "" } };

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Champions.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Champions.Count(), 1);
                //Act
                context.Champions.Remove(item);
                context.SaveChanges();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(context.Champions.Count(), 0);

            }
        }

        [TestMethod]
        public async Task TestGetChampion()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            ChampionEntity item = new ChampionEntity { Name = "Blabla", Bio = "", Class = "Tank", Icon = "", Image = new ImageEntity { base64 = "" } };
            List<ChampionEntity> lesChampions;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Champions.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Champions.Count(), 1);
                //Act
                lesChampions = context.Champions.ToList();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(lesChampions.Count(), 1);
            }
        }

        [TestMethod]
        public async Task TestGetChampionByName()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            ChampionEntity item = new ChampionEntity { Name = "Blabla", Bio = "", Class = "Tank", Icon = "", Image = new ImageEntity { base64 = "" } };
            List<ChampionEntity> lesChampions;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Champions.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Champions.Count(), 1);
                //Act
                lesChampions = context.Champions.Where(c => c.Name == item.Name).ToList();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(lesChampions.Count(), 1);
            }
        }

        [TestMethod]
        public async Task TestGetNbChampion()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            ChampionEntity item = new ChampionEntity { Name = "Blabla", Bio = "", Class = "Tank", Icon = "" , Image = new ImageEntity { base64 = "" } };
            int nbChampions = 0;
            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Champions.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Champions.Count(), 1);
                //Act
                nbChampions = await context.Champions.CountAsync();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(nbChampions, 1);
            }
        }

        [TestMethod]
        public async Task TestGetNbChampionByName()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            ChampionEntity item = new ChampionEntity { Name = "Blabla", Bio = "", Class = "Tank", Icon = "" , Image = new ImageEntity { base64 = "" } };
            int nbChampion = 0;
            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Champions.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Champions.Count(), 1);
                //Act
                nbChampion = await context.Champions.Where(c => c.Name == item.Name).CountAsync();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert

                Assert.AreEqual(nbChampion,1);
            }
        }

        [TestMethod]
        public async Task TestUpdateChampion()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            ChampionEntity item = new ChampionEntity { Name = "Blabla", Bio = "", Class = "Tank", Icon = "" , Image = new ImageEntity { base64 = "" } };
            List<ChampionEntity> lesChampions;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Champions.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Champions.Count(), 1);
                //Act
                item.Bio = "tata";
                context.Champions.Update(item);
                context.SaveChanges();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                lesChampions = context.Champions.Where(c => c.Name == item.Name).ToList();
                Assert.AreEqual(lesChampions.FirstOrDefault().Bio, "tata");
            }
        }
    }
}