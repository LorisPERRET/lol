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
                Image = skin.Image.Base64,
                Champion = skin.Champion.ToEntity(),
            };
        }

        public static IEnumerable<SkinEntity> ToEntities(this IEnumerable<Skin> skin)
        {
            return skin.Select(s => ToEntity(s));
        }
    }
}
