using DTO_EF;
using DTO_EF.Mapper;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDb
{
    public class BDDSkins : ISkinsManager
    {
        public SqlLiteDbContext _dbContext { get; }

        public BDDSkins(SqlLiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Skin?> AddItem(Skin? item)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                var res = await context.Skins.AddAsync(item.ToEntity());
                context.SaveChanges();

                return res.Entity.ToSkin();
            }
        }

        public async Task<bool> DeleteItem(Skin? item)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                var res = context.Skins.Remove(item.ToEntity());
                context.SaveChanges();

                return (res != null);
            }
        }

        public async Task<IEnumerable<Skin?>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<SkinEntity> temp = context.Skins.ToList();
                if (orderingPropertyName != null)
                {
                    var prop = typeof(SkinEntity).GetProperty(orderingPropertyName!);
                    if (prop != null)
                    {
                        temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                            : temp.OrderBy(item => prop.GetValue(item));
                    }
                }
                return temp.Skip(index * count).Take(count).ToSkins();
            }
        }

        public async Task<IEnumerable<Skin?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<SkinEntity> temp = context.Skins.ToList();
                temp = temp.Where(item => item.Name == substring);
                if (orderingPropertyName != null)
                {
                    var prop = typeof(Champion).GetProperty(orderingPropertyName!);
                    if (prop != null)
                    {
                        temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                            : temp.OrderBy(item => prop.GetValue(item));
                    }
                }
                return temp.Skip(index * count).Take(count).ToSkins();
            }
        }

        public async Task<IEnumerable<Skin?>> GetItemsByChampion(Champion? champion, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<SkinEntity> temp = context.Skins.ToList();
                temp = temp.Where(item => item.Champion == champion.ToEntity());
                if (orderingPropertyName != null)
                {
                    var prop = typeof(Champion).GetProperty(orderingPropertyName!);
                    if (prop != null)
                    {
                        temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                            : temp.OrderBy(item => prop.GetValue(item));
                    }
                }
                return temp.Skip(index * count).Take(count).ToSkins();
            }
        }

        public async Task<int> GetNbItems()
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.Skins.CountAsync();
            }
        }

        public async Task<int> GetNbItemsByName(string substring)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.Skins.Where(item => item.Name == substring).CountAsync();
            }
        }

        public async Task<int> GetNbItemsByChampion(Champion? champion)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.Skins.Where(item => item.Champion == champion.ToEntity()).CountAsync();
            }
        }

        public async Task<Skin?> UpdateItem(Skin? oldItem, Skin? newItem)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                if (oldItem == null) return null;
                if (newItem == null) return null;

                var runePage = context.Skins.Where(item => item.Name == oldItem.Name);
                if (runePage.Count() > 1 || runePage.Count() == 0) return null;

                var res = context.Update(newItem);

                context.SaveChanges();
                return res.Entity;
            }
        }
    }
}
