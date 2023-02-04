using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_EF;
using DTO_EF.Mapper;
using Model;
using Shared;

namespace EntityFramework
{
    public class DbDataManager : IGenericDataManager<Champion>
    {
        public Task<Champion> AddItem(Champion item)
        {
            using (var context = new SqlLiteDbContext())
            {
                context.Champions.Add(item.ToEntity());
                return Task.FromResult<Champion>(item);
            }
        }

        public Task<bool> DeleteItem(Champion item)
        {
            using (var context = new SqlLiteDbContext())
            {
                var result = context.Champions.Remove(item.ToEntity());
                if (result == null)
                {
                    return Task.FromResult<bool>(false);
                }

                return Task.FromResult<bool>(true);
            }
        }

        public Task<IEnumerable<Champion>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Champion>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
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
    }
}
