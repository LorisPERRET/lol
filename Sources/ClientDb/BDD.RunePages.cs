using DTO_EF;
using DTO_EF.Mapper;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDb
{
    public class BDDRunePages : IRunePagesManager
    {
        public SqlLiteDbContext _dbContext { get; }

        public BDDRunePages(SqlLiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RunePage?> AddItem(RunePage? item)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                var res = await context.RunePages.AddAsync(item.ToEntity());
                context.SaveChanges();

                return res.Entity.ToRunePage();
            }
        }

        public async Task<bool> DeleteItem(RunePage? item)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                var res = context.RunePages.Remove(item.ToEntity());
                context.SaveChanges();

                return (res != null);
            }
        }

        public async Task<IEnumerable<RunePage?>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<RunePageEntity> temp = context.RunePages.ToList();
                if (orderingPropertyName != null)
                {
                    var prop = typeof(RunePageEntity).GetProperty(orderingPropertyName!);
                    if (prop != null)
                    {
                        temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                            : temp.OrderBy(item => prop.GetValue(item));
                    }
                }
                return temp.Skip(index * count).Take(count).ToRunePages();
            }
        }

        public async Task<IEnumerable<RunePage?>> GetItemsByChampion(Champion? champion, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<RunePageEntity> temp = context.RunePages.ToList();
                temp = temp.Where(item => item.Champions.Contains(champion.ToEntity()));
                if (orderingPropertyName != null)
                {
                    var prop = typeof(Champion).GetProperty(orderingPropertyName!);
                    if (prop != null)
                    {
                        temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                            : temp.OrderBy(item => prop.GetValue(item));
                    }
                }
                return temp.Skip(index * count).Take(count).ToRunePages();
            }
        }

        public async Task<IEnumerable<RunePage?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<RunePageEntity> temp = context.RunePages.ToList();
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
                return temp.Skip(index * count).Take(count).ToRunePages();
            }
        }

        public async  Task<IEnumerable<RunePage?>> GetItemsByRune(Model.Rune? rune, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<RunePageEntity> temp = context.RunePages.ToList();
                temp = temp.Where(item => item.Runes.Contains(rune.ToEntity()));
                if (orderingPropertyName != null)
                {
                    var prop = typeof(Champion).GetProperty(orderingPropertyName!);
                    if (prop != null)
                    {
                        temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                            : temp.OrderBy(item => prop.GetValue(item));
                    }
                }
                return temp.Skip(index * count).Take(count).ToRunePages();
            }
        }

        public async Task<int> GetNbItems()
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.RunePages.CountAsync();
            }
        }

        public async Task<int> GetNbItemsByChampion(Champion? champion)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.RunePages.Where(item => item.Champions.Contains(champion.ToEntity())).CountAsync();
            }
        }

        public async Task<int> GetNbItemsByName(string substring)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.RunePages.Where(item => item.Name == substring).CountAsync();
            }
        }

        public async Task<int> GetNbItemsByRune(Model.Rune? rune)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.RunePages.Where(item => item.Runes.Contains(rune.ToEntity())).CountAsync();
            }
        }

        public async Task<RunePage?> UpdateItem(RunePage? oldItem, RunePage? newItem)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                if (oldItem == null) return null;
                if (newItem == null) return null;

                var runePage = context.RunePages.Where(item => item.Name == oldItem.Name);
                if (runePage.Count() > 1 || runePage.Count() == 0) return null;

                var res = context.Update(newItem);

                context.SaveChanges();
                return res.Entity;
            }
        }
    }
}
