using DTO_EF;
using DTO_EF.Mapper;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rune = Model.Rune;

namespace ClientDb
{
    public class BDDRunes : IRunesManager
    {
        public SqlLiteDbContext _dbContext { get; }

        public BDDRunes(SqlLiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Rune?> AddItem(Rune? item)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                var res = await context.Runes.AddAsync(item.ToEntity());
                context.SaveChanges();

                return res.Entity.ToRune();
            }
        }

        public async Task<bool> DeleteItem(Rune? item)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                var res = context.Runes.Remove(item.ToEntity());
                context.SaveChanges();

                return (res != null);
            }
        }

        public async Task<IEnumerable<Rune?>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<RuneEntity> temp = context.Runes.ToList();
                if (orderingPropertyName != null)
                {
                    var prop = typeof(RuneEntity).GetProperty(orderingPropertyName!);
                    if (prop != null)
                    {
                        temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                            : temp.OrderBy(item => prop.GetValue(item));
                    }
                }
                return temp.Skip(index * count).Take(count).ToRunes();
            }
        }

        public async Task<IEnumerable<Rune?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<RuneEntity> temp = context.Runes.ToList();
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
                return temp.Skip(index * count).Take(count).ToRunes();
            }
        }

        public async Task<IEnumerable<Rune?>> GetItemsByFamily(RuneFamily family, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<RuneEntity> temp = context.Runes.ToList();
                temp = temp.Where(item => item.Familly == family.ToString());
                if (orderingPropertyName != null)
                {
                    var prop = typeof(Champion).GetProperty(orderingPropertyName!);
                    if (prop != null)
                    {
                        temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                            : temp.OrderBy(item => prop.GetValue(item));
                    }
                }
                return temp.Skip(index * count).Take(count).ToRunes();
            }
        }

        public async Task<int> GetNbItems()
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.Runes.CountAsync();
            }
        }

        public async Task<int> GetNbItemsByName(string substring)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.Runes.Where(item => item.Name == substring).CountAsync();
            }
        }

        public async Task<int> GetNbItemsByFamily(RuneFamily family)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.Runes.Where(item => item.Familly == family.ToString()).CountAsync();
            }
        }

        public async Task<Rune?> UpdateItem(Rune? oldItem, Rune? newItem)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                if (oldItem == null) return null;
                if (newItem == null) return null;

                var runePage = context.Runes.Where(item => item.Name == oldItem.Name);
                if (runePage.Count() > 1 || runePage.Count() == 0) return null;

                var res = context.Update(newItem);

                context.SaveChanges();
                return res.Entity;
            }
        }
    }
}
