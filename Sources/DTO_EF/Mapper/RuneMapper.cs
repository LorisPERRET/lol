using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rune = Model.Rune;

namespace DTO_EF.Mapper
{
    public static class RuneMapper
    {
        public static RuneEntity ToEntity(this Rune rune)
        {
            return new RuneEntity
            {
                Name = rune.Name,
                Description = rune.Description,
                Familly = rune.Family.ToString(),
                Image = rune.Image.Base64
            };
        }

        public static IEnumerable<RuneEntity> ToEntities(this IEnumerable<Rune> rune)
        {
            return rune.Select(r => ToEntity(r));
        }
    }
}
