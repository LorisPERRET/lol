using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_EF.Mapper
{
    public static class ImageMapper
    {
        public static ImageEntity ToEntity(this LargeImage image)
        {
            return new ImageEntity
            {
                base64 = image.Base64
            };
        }

        public static IEnumerable<ImageEntity> ToEntities(this IEnumerable<LargeImage> images)
        {
            return images.Select(i => ToEntity(i));
        }
    }
}
