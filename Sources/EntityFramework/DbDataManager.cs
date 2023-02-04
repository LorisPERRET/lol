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
                context.Database.EnsureCreated();
                context.Champions.Add(item.ToEntity());
                context.SaveChanges();
                return Task.FromResult<Champion>(item);
            }
        }

        public Task<bool> DeleteItem(Champion item)
        {
            using (var context = new SqlLiteDbContext())
            {
                context.Database.EnsureCreated();
                var result = context.Champions.Remove(item.ToEntity());
                context.SaveChanges();
                return Task.FromResult<bool>(result != null);
            }
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
            using (var context = new SqlLiteDbContext())
            {
                var temp = context.Champions.Where(c => c.Name == substring).Skip(index * count).Take(count);
                temp = GetItemWithFilter(orderingPropertyName, descending, temp);
                return Task.FromResult(temp.ToList().ToChampions());
            }
        }

        public Task<int> GetNbItems()
        {
            using (var context = new SqlLiteDbContext())
            {
                context.Database.EnsureCreated();
                var count = context.Champions.Count();
                return Task.FromResult<int>(count);
            }
        }

        public Task<int> GetNbItemsByName(string substring)
        {
            using (var context = new SqlLiteDbContext())
            {
                context.Database.EnsureCreated();
                var count = context.Champions.Count(item => item.Name == substring);
                return Task.FromResult<int>(count);
            }
        }

        public Task<Champion> UpdateItem(Champion oldItem, Champion newItem)
        {
            using (var context = new SqlLiteDbContext())
            {
                context.Database.EnsureCreated();
                if (oldItem == null || newItem == null) return Task.FromResult<Champion>(default(Champion));

                if (!context.Champions.Contains(oldItem.ToEntity()))
                {
                    return Task.FromResult<Champion>(default(Champion));
                }

                context.Champions.Remove(oldItem.ToEntity());
                context.Champions.Add(newItem.ToEntity());
                context.SaveChanges();
                return Task.FromResult<Champion>(newItem);
            }
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
