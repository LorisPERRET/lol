using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_EF
{
    public class RunePageEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ChampionEntity> Champions { get; set; } = new List<ChampionEntity>();

        public ICollection<RuneEntity> Runes { get; set; } = new List<RuneEntity>();
    }
}
