using DTO_EF;
using DTO_EF.Mapper;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDb
{
    public class BDDChampions : IChampionsManager
    {
        public SqlLiteDbContext _dbContext { get; }

        public BDDChampions(SqlLiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Champion?> AddItem(Champion? item)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                var res = await context.Champions.AddAsync(item.ToEntity());
                context.SaveChanges();

                return res.Entity.ToChampion();
            }
        }

        public async Task<bool> DeleteItem(Champion? item)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();
                
                var res = context.Champions.Remove(item.ToEntity());
                context.SaveChanges();

                return (res != null);
            }
        }

        public async Task<IEnumerable<Champion?>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<ChampionEntity> temp = context.Champions.ToList();
                if (orderingPropertyName != null)
                {
                    var prop = typeof(ChampionEntity).GetProperty(orderingPropertyName!);
                    if (prop != null)
                    {
                        temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                            : temp.OrderBy(item => prop.GetValue(item));
                    }
                }
                return temp.Skip(index * count).Take(count).ToChampions();
            }
        }

        public Task<IEnumerable<Champion?>> GetItemsByCharacteristic(string charName, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Champion?>> GetItemsByClass(ChampionClass championClass, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<ChampionEntity> temp = context.Champions.ToList();
                temp = temp.Where(item => item.Class == championClass.ToString());
                if (orderingPropertyName != null)
                {
                    var prop = typeof(Champion).GetProperty(orderingPropertyName!);
                    if (prop != null)
                    {
                        temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                            : temp.OrderBy(item => prop.GetValue(item));
                    }
                }
                return temp.Skip(index * count).Take(count).ToChampions();
            }
        }

        public async Task<IEnumerable<Champion?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<ChampionEntity> temp = context.Champions.ToList();
                temp = temp.Where(item => item.Name == substring);
                if (orderingPropertyName != null)
                {
                    var prop = typeof(ChampionEntity).GetProperty(orderingPropertyName!);
                    if (prop != null)
                    {
                        temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                            : temp.OrderBy(item => prop.GetValue(item));
                    }
                }
                return temp.Skip(index * count).Take(count).ToChampions();
            }
        }

        public async Task<IEnumerable<Champion?>> GetItemsByRunePage(RunePage? runePage, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                IEnumerable<ChampionEntity> temp = context.Champions.ToList();
                temp = temp.Where(item => item.RunePages.Contains(runePage.ToEntity()));
                if (orderingPropertyName != null)
                {
                    var prop = typeof(Champion).GetProperty(orderingPropertyName!);
                    if (prop != null)
                    {
                        temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                            : temp.OrderBy(item => prop.GetValue(item));
                    }
                }
                return temp.Skip(index * count).Take(count).ToChampions();
            }
        }

        public Task<IEnumerable<Champion?>> GetItemsBySkill(Skill? skill, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Champion?>> GetItemsBySkill(string skill, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNbItems()
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.Champions.CountAsync();
            }
        }

        public Task<int> GetNbItemsByCharacteristic(string charName)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNbItemsByClass(ChampionClass championClass)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.Champions.Where(item => item.Class == championClass.ToString()).CountAsync();
            }
        }

        public async Task<int> GetNbItemsByName(string substring)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.Champions.Where(item => item.Name == substring ).CountAsync();
            }
        }

        public async Task<int> GetNbItemsByRunePage(RunePage? runePage)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                return await context.Champions.Where(item => item.RunePages.Contains(runePage.ToEntity())).CountAsync();
            }
        }

        public Task<int> GetNbItemsBySkill(Skill? skill)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsBySkill(string skill)
        {
            throw new NotImplementedException();
        }

        public async Task<Champion?> UpdateItem(Champion? oldItem, Champion? newItem)
        {
            using (var context = _dbContext)
            {
                context.Database.EnsureCreated();

                if (oldItem == null) return null;
                if (newItem == null) return null;

                var champ = context.Champions.Where(item => item.Name == oldItem.Name);
                if (champ.Count() > 1 || champ.Count() == 0 ) return null;

                var res = context.Update(newItem);
                          
                context.SaveChanges();
                return res.Entity;
            }
        }
    }
}
