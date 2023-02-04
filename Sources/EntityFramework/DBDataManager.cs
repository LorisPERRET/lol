using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_EF;
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
