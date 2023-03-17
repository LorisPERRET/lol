using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_EF
{
    public class ChampionEntity
    {
        [Key]
        public string Name { get; set; }

        public string Bio { get; set; }

        public string Icon { get; set; }

        public string Class { get; set; }

        public ImageEntity Image { get; set; }

        public ICollection<SkinEntity> Skins { get; set; } = new List<SkinEntity>();

        public ICollection<RunePageEntity> RunePages { get; set; } = new List<RunePageEntity>();
    }
}
