using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_EF.Mapper
{
    public static class SkinMapper
    {
        public static SkinEntity ToEntity(this Skin skin)
        {
            return new SkinEntity
            {
                Name = skin.Name,
                Description = skin.Description,
                Icon = skin.Icon,
                Price = skin.Price,
            };
        }

        public static IEnumerable<SkinEntity> ToEntities(this IEnumerable<Skin> skin)
        {
            return skin.Select(s => ToEntity(s));
        }

        public static Skin ToSkin(this SkinEntity skin)
        {
            return new Skin(skin.Name, skin.Champion.ToChampion(), skin.Price, skin.Image.base64, skin.Description);
        }

        public static IEnumerable<Skin> ToSkins(this IEnumerable<SkinEntity> skin)
        {
            return skin.Select(s => ToSkin(s));
        }
    }
}
