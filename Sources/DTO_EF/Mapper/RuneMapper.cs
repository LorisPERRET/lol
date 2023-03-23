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
            };
        }

        public static IEnumerable<RuneEntity> ToEntities(this IEnumerable<Rune> rune)
        {
            return rune.Select(r => ToEntity(r));
        }

        public static Rune ToRune(this RuneEntity rune)
        {
            return new Rune(rune.Name, Enum.Parse<RuneFamily>(rune.Familly), "", rune.Image.base64, rune.Description);
        }

        public static IEnumerable<Rune> ToRunes(this IEnumerable<RuneEntity> rune)
        {
            return rune.Select(r => ToRune(r));
        }
    }
}
