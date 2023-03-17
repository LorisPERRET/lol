using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_EF
{
    public class SkinEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public float Price { get; set; }

        public ImageEntity Image { get; set; }

        public ChampionEntity Champion { get; set; }

    }
}
