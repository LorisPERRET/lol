using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_API
{
    public class SkinDto
    {
        public string Name { get ; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public float Price { get; set; }
        public ChampionDto Champion { get; set; }
        public LargeImageDto Image { get; set; }
    }
}
