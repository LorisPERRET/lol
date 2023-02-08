using DTO_EF;
using DTO_EF.Mapper;
using Model;
using Shared;

namespace EntityFramework
{
    public class DbDataManager : IGenericDataManager<Champion>
    {
        private SqlLiteDbContext _context;

        public DbDataManager(SqlLiteDbContext context)
        {
            _context = context;
        }

        public Task<Champion> AddItem(Champion item)
        {
            _context.Champions.Add(item.ToEntity());
            return Task.FromResult<Champion>(item);
        }

        public Task<bool> DeleteItem(Champion item)
        {
            var result = _context.Champions.Remove(item.ToEntity());
            return Task.FromResult<bool>(result != null);
        }

        public Task<IEnumerable<Champion>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            var temp = _context.Champions.Skip(index * count).Take(count);
            temp = GetItemWithFilter(orderingPropertyName, descending, temp);
            return Task.FromResult(temp.ToList().ToChampions());
        }

        public Task<IEnumerable<Champion>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            var temp = _context.Champions.Where(c => c.Name == substring).Skip(index * count).Take(count);
            temp = GetItemWithFilter(orderingPropertyName, descending, temp);
            return Task.FromResult(temp.ToList().ToChampions());
        }

        public Task<int> GetNbItems()
        {
            var count = _context.Champions.Count();
            return Task.FromResult<int>(count);
        }

        public Task<int> GetNbItemsByName(string substring)
        {
            var count = _context.Champions.Count(item => item.Name == substring);
            return Task.FromResult<int>(count);
        }

        public Task<Champion> UpdateItem(Champion oldItem, Champion newItem)
        {
            if (oldItem == null || newItem == null) return Task.FromResult<Champion>(default(Champion));

            if (!_context.Champions.Contains(oldItem.ToEntity()))
            {
                return Task.FromResult<Champion>(default(Champion));
            }

            _context.Champions.Remove(oldItem.ToEntity());
            _context.Champions.Add(newItem.ToEntity());
            return Task.FromResult<Champion>(newItem);
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
