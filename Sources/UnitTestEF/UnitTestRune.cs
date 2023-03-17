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
    public class UnitTestRune
    {
        [TestMethod]
        public async Task TestAddRune()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RuneEntity item = new RuneEntity { Name = "Stinger", Description = "", Image = new ImageEntity { base64 = "" }, Familly = RuneFamily.Precision.ToString() };

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();

                //Act
                await context.Runes.AddAsync(item);
                context.SaveChanges();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(context.Runes.Count(), 1);
                List<RuneEntity> lesRunes = context.Runes.Where(c => c.Name == item.Name).ToList();
                Assert.AreEqual(lesRunes.FirstOrDefault().Name, item.Name);
            }
        }

        [TestMethod]
        public async Task TestDeleteRune()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RuneEntity item = new RuneEntity { Name = "Stinger", Description = "", Image = new ImageEntity { base64 = "" }, Familly = RuneFamily.Precision.ToString() };

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Runes.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Runes.Count(), 1);
                //Act
                context.Runes.Remove(item);
                context.SaveChanges();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(context.Runes.Count(), 0);

            }
        }

        [TestMethod]
        public async Task TestGetRune()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RuneEntity item = new RuneEntity { Name = "Stinger", Description = "", Image = new ImageEntity { base64 = "" }, Familly = RuneFamily.Precision.ToString() };
            List<RuneEntity> lesRunes;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Runes.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Runes.Count(), 1);
                //Act
                lesRunes = context.Runes.ToList();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(lesRunes.Count(), 1);
            }
        }

        [TestMethod]
        public async Task TestGetRunesByName()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RuneEntity item = new RuneEntity { Name = "Stinger", Description = "", Image = new ImageEntity { base64 = "" }, Familly = RuneFamily.Precision.ToString() };
            List<RuneEntity> lesRunes;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Runes.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Runes.Count(), 1);
                //Act
                lesRunes = context.Runes.Where(c => c.Name == item.Name).ToList();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(lesRunes.Count(), 1);
            }
        }

        [TestMethod]
        public async Task TestGetNbRunes()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RuneEntity item = new RuneEntity { Name = "Stinger", Description = "", Image = new ImageEntity { base64 = "" }, Familly = RuneFamily.Precision.ToString() };
            int nbRunes = 0;
            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Runes.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Runes.Count(), 1);
                //Act
                nbRunes = await context.Runes.CountAsync();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(nbRunes, 1);
            }
        }

        [TestMethod]
        public async Task TestGetNbRunesByName()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            RuneEntity item = new RuneEntity { Name = "Stinger", Description = "", Image = new ImageEntity { base64 = "" }, Familly = RuneFamily.Precision.ToString() };
            int nbRunes = 0;
            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Runes.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Runes.Count(), 1);
                //Act
                nbRunes = await context.Runes.Where(c => c.Name == item.Name).CountAsync();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert

                Assert.AreEqual(nbRunes, 1);
            }
        }

        [TestMethod]
        public async Task TestUpdateRune()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arang
            RuneEntity item = new RuneEntity { Name = "Stinger", Description = "", Image = new ImageEntity { base64 = "" }, Familly = RuneFamily.Precision.ToString() };
            List<RuneEntity> lesRunes;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Runes.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Runes.Count(), 1);
                //Act
                item.Description = "tata";
                context.Runes.Update(item);
                context.SaveChanges();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                lesRunes = context.Runes.Where(c => c.Name == item.Name).ToList();
                Assert.AreEqual(lesRunes.FirstOrDefault().Description, "tata");
            }
        }
    }
}
