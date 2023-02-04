using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_EF;
using DTO_EF.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model;
using Shared;

namespace EntityFramework
{
    public class DBDataManager : IGenericDataManager<Champion>
    {
        public Task<Champion> AddItem(Champion item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItem(Champion item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Champion>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using (var context = new SqlLiteDbContext())
            {
                var temp = context.Champions.Skip(index * count).Take(count);
                temp = GetItemWithFilter(orderingPropertyName, descending, temp);
                return Task.FromResult(temp.ToList().ToChampions());
            }
        }

        public Task<IEnumerable<Champion>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            using(var context = new SqlLiteDbContext())
            {
                var temp = context.Champions.Where(c => c.Name == substring).Skip(index * count).Take(count);
                temp = GetItemWithFilter(orderingPropertyName, descending, temp);
                return Task.FromResult(temp.ToList().ToChampions());
            }
        }

        public Task<int> GetNbItems()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsByName(string substring)
        {
            throw new NotImplementedException();
        }

        public Task<Champion> UpdateItem(Champion oldItem, Champion newItem)
        {
            throw new NotImplementedException();
        }

        private static IQueryable<ChampionEntity> GetItemWithFilter(string? orderingPropertyName, bool descending, IQueryable<ChampionEntity> temp)
        {
            if (orderingPropertyName != null)
            {
                var prop = typeof(ChampionEntity).GetProperty(orderingPropertyName!);
                if (prop != null)
                {
                    temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                        : temp.OrderBy(item => prop.GetValue(item));
                }
            }

            return temp;
        }

    }
}
