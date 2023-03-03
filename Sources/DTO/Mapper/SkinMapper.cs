using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_API.Mapper
{
    public static class SkinMapper
    {
        public static SkinDto ToDto(this Skin skin)
        {
            return new SkinDto
            {
                Name = skin.Name,
                Icon = skin.Icon,
                Description = skin.Description,
                Price = skin.Price,
                Champion = skin.Champion.ToDto(),
                Image = skin.Image.ToDto(),
            };
        }

        public static IEnumerable<SkinDto> ToDtos(this IEnumerable<Skin> skins)
        {
            return skins.Select(s => ToDto(s));
        }

        public static Skin ToSkin(this SkinDto skin)
        {
            return new Skin(skin.Name, skin.Champion.ToChampion(), skin.Price, skin.Icon, skin.Image.Base64, skin.Description);
        }
    }
}
