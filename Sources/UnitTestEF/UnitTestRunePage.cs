using DTO_EF;
using EntityFramework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestEF
{
    [TestClass]
    public class UnitTestRunePage
    {
        [TestMethod]
        public async Task TestAddRunePage()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RunePageEntity item = new RunePageEntity { Name = "Stinger" };

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();

                //Act
                await context.RunePages.AddAsync(item);
                context.SaveChanges();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(context.RunePages.Count(), 1);
                List<RunePageEntity> lesRunePages = context.RunePages.Where(c => c.Name == item.Name).ToList();
                Assert.AreEqual(lesRunePages.FirstOrDefault().Name, item.Name);
            }
        }

        [TestMethod]
        public async Task TestDeleteRunePage()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RunePageEntity item = new RunePageEntity {Name = "Stinger" };

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.RunePages.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.RunePages.Count(), 1);
                //Act
                context.RunePages.Remove(item);
                context.SaveChanges();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(context.RunePages.Count(), 0);

            }
        }

        [TestMethod]
        public async Task TestGetRunePage()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RunePageEntity item = new RunePageEntity {Name = "Stinger" };
            List<RunePageEntity> lesRunePages;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.RunePages.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.RunePages.Count(), 1);
                //Act
                lesRunePages = context.RunePages.ToList();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(lesRunePages.Count(), 1);
            }
        }

        [TestMethod]
        public async Task TestGetRunePagesByName()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RunePageEntity item = new RunePageEntity {Name = "Stinger" };
            List<RunePageEntity> lesRunePages;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.RunePages.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.RunePages.Count(), 1);
                //Act
                lesRunePages = context.RunePages.Where(c => c.Name == item.Name).ToList();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(lesRunePages.Count(), 1);
            }
        }

        [TestMethod]
        public async Task TestGetRunePagesByRune()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RuneEntity rune = new RuneEntity { Name = "Stinger", Description = "", Image = new ImageEntity { base64 = "" }, Familly = RuneFamily.Precision.ToString() };
            RunePageEntity item = new RunePageEntity { Name = "Stinger", Runes = new List<RuneEntity>() { rune } };
            List<RunePageEntity> lesRunePages;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.RunePages.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.RunePages.Count(), 1);
                //Act
                lesRunePages = context.RunePages.Where(c => c.Runes.Contains(rune)).ToList();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(lesRunePages.Count(), 1);
            }
        }

        [TestMethod]
        public async Task TestGetRunePagesByChampion()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            ChampionEntity champion = new ChampionEntity { Name = "Blabla", Bio = "", Class = "Tank", Icon = "", Image = new ImageEntity { base64 = "" } };
            RunePageEntity item = new RunePageEntity { Name = "Stinger", Champions = new List<ChampionEntity>() { champion } };
            List<RunePageEntity> lesRunePages;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.RunePages.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.RunePages.Count(), 1);
                //Act
                lesRunePages = context.RunePages.Where(c => c.Champions.Contains(champion)).ToList();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(lesRunePages.Count(), 1);
            }
        }

        [TestMethod]
        public async Task TestGetNbRunePages()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RunePageEntity item = new RunePageEntity {Name = "Stinger" };
            int nbRunePages = 0;
            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.RunePages.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.RunePages.Count(), 1);
                //Act
                nbRunePages = await context.RunePages.CountAsync();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(nbRunePages, 1);
            }
        }

        [TestMethod]
        public async Task TestGetNbRunePagesByName()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RunePageEntity item = new RunePageEntity {Name = "Stinger" };
            int nbRunePages = 0;
            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.RunePages.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.RunePages.Count(), 1);
                //Act
                nbRunePages = await context.RunePages.Where(c => c.Name == item.Name).CountAsync();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert

                Assert.AreEqual(nbRunePages, 1);
            }
        }

        [TestMethod]
        public async Task TestGetNbRunePagesByRune()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RuneEntity rune = new RuneEntity { Name = "Stinger", Description = "", Image = new ImageEntity { base64 = "" }, Familly = RuneFamily.Precision.ToString() };
            RunePageEntity item = new RunePageEntity { Name = "Stinger", Runes = new List<RuneEntity>() { rune } };
            int nbRunePages = 0;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.RunePages.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.RunePages.Count(), 1);
                //Act
                nbRunePages = await context.RunePages.Where(c => c.Runes.Contains(rune)).CountAsync();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(nbRunePages, 1);
            }
        }

        [TestMethod]
        public async Task TestGetNbRunePagesByChampion()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            ChampionEntity champion = new ChampionEntity { Name = "Blabla", Bio = "", Class = "Tank", Icon = "", Image = new ImageEntity { base64 = "" } };
            RunePageEntity item = new RunePageEntity { Name = "Stinger", Champions = new List<ChampionEntity>() { champion } };
            int nbRunePages = 0;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.RunePages.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.RunePages.Count(), 1);
                //Act
                nbRunePages = await context.RunePages.Where(c => c.Champions.Contains(champion)).CountAsync();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(nbRunePages, 1);
            }
        }

        [TestMethod]
        public async Task TestUpdateRunePage()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arang
            RunePageEntity item = new RunePageEntity {Name = "Stinger" };
            List<RunePageEntity> lesRunePages;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.RunePages.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.RunePages.Count(), 1);
                //Act
                item.Name = "tata";
                context.RunePages.Update(item);
                context.SaveChanges();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                lesRunePages = context.RunePages.Where(c => c.Id == item.Id).ToList();
                Assert.AreEqual(lesRunePages.FirstOrDefault().Name, "tata");
            }
        }
    }
}
