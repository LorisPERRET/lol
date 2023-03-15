using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDb
{
    public class BDDRunes : IRunesManager
    {
        public DbContext _dbContext { get; }

        public BDDRunes(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Model.Rune?> AddItem(Model.Rune? item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItem(Model.Rune? item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Model.Rune?>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Model.Rune?>> GetItemsByFamily(RuneFamily family, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Model.Rune?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItems()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsByFamily(RuneFamily family)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsByName(string substring)
        {
            throw new NotImplementedException();
        }

        public Task<Model.Rune?> UpdateItem(Model.Rune? oldItem, Model.Rune? newItem)
        {
            throw new NotImplementedException();
        }
    }
}
