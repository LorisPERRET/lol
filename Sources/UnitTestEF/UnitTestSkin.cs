using DTO_EF;
using EntityFramework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestEF
{
    [TestClass]
    public class UnitTestSkin
    {
        [TestMethod]
        public async Task TestAddSkin()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            SkinEntity item = new SkinEntity { Name = "Stinger", Description = "", Icon = "", Image = "", Price = 0f };

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();

                //Act
                await context.Skins.AddAsync(item);
                context.SaveChanges();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(context.Skins.Count(), 1);
                List<SkinEntity> lesSkins = context.Skins.Where(c => c.Name == item.Name).ToList();
                Assert.AreEqual(lesSkins.FirstOrDefault().Name, item.Name);
            }
        }

        [TestMethod]
        public async Task TestDeleteSkin()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            SkinEntity item = new SkinEntity { Name = "Stinger", Description = "", Icon = "", Image = "", Price = 0f };

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Skins.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Skins.Count(), 1);
                //Act
                context.Skins.Remove(item);
                context.SaveChanges();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(context.Skins.Count(), 0);

            }
        }

        [TestMethod]
        public async Task TestGetSkin()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            SkinEntity item = new SkinEntity { Name = "Stinger", Description = "", Icon = "", Image = "", Price = 0f };
            List<SkinEntity> lesSkins;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Skins.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Skins.Count(), 1);
                //Act
                lesSkins = context.Skins.ToList();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(lesSkins.Count(), 1);
            }
        }

        [TestMethod]
        public async Task TestGetSkinsByName()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            SkinEntity item = new SkinEntity { Name = "Stinger", Description = "", Icon = "", Image = "", Price = 0f };
            List<SkinEntity> lesSkins;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Skins.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Skins.Count(), 1);
                //Act
                lesSkins = context.Skins.Where(c => c.Name == item.Name).ToList();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(lesSkins.Count(), 1);
            }
        }

        [TestMethod]
        public async Task TestGetNbSkins()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            SkinEntity item = new SkinEntity { Name = "Stinger", Description = "", Icon = "", Image = "", Price = 0f };
            int nbSkins = 0;
            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Skins.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Skins.Count(), 1);
                //Act
                nbSkins = await context.Skins.CountAsync();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                Assert.AreEqual(nbSkins, 1);
            }
        }

        [TestMethod]
        public async Task TestGetNbSkinsByName()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arange
            SkinEntity item = new SkinEntity { Name = "Stinger", Description = "", Icon = "", Image = "", Price = 0f };
            int nbSkins = 0;
            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Skins.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Skins.Count(), 1);
                //Act
                nbSkins = await context.Skins.Where(c => c.Name == item.Name).CountAsync();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert

                Assert.AreEqual(nbSkins, 1);
            }
        }

        [TestMethod]
        public async Task TestUpdateSkin()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SqlLiteDbContext>()
                                .UseSqlite(connection)
                                .Options;

            //Arang
            SkinEntity item = new SkinEntity { Name = "Stinger", Description = "", Icon = "", Image = "", Price = 0f };
            List<SkinEntity> lesSkins;

            using (var context = new SqlLiteDbContext(options))
            {
                context.Database.EnsureCreated();
                await context.Skins.AddAsync(item);
                context.SaveChanges();
                Assert.AreEqual(context.Skins.Count(), 1);
                //Act
                item.Description = "tata";
                context.Skins.Update(item);
                context.SaveChanges();
            }

            using (var context = new SqlLiteDbContext(options))
            {
                //Assert
                lesSkins = context.Skins.Where(c => c.Name == item.Name).ToList();
                Assert.AreEqual(lesSkins.FirstOrDefault().Description, "tata");
            }
        }
    }
}
