using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DTO_API.Mapper
{
    public static class LargeImageMapper
    {
        public static LargeImageDto ToDto(this LargeImage image)
        {
            return new LargeImageDto
            {
                Base64 = image.Base64
            };
        }

        public static IEnumerable<LargeImageDto> ToDtos(this IEnumerable<LargeImage> images)
        {
            return images.Select(i => ToDto(i));
        }

        public static LargeImage ToLargeImage(this LargeImageDto image)
        {
            return new LargeImage(image.Base64);
        }
    }
}
