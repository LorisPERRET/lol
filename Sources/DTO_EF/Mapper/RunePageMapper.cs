using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_EF.Mapper
{
    public static class RunePageMapper
    {
        public static RunePageEntity ToEntity(this RunePage runePage)
        {
            return new RunePageEntity
            {
                Name = runePage.Name
            };
        }

        public static IEnumerable<RunePageEntity> ToEntities(this IEnumerable<RunePage> runePage)
        {
            return runePage.Select(r => ToEntity(r));
        }

        public static RunePage ToRunePage(this RunePageEntity runePage)
        {
            return new RunePage(runePage.Name);
        }

        public static IEnumerable<RunePage> ToRunePages(this IEnumerable<RunePageEntity> runePage)
        {
            return runePage.Select(r => ToRunePage(r));
        }
    }
}
