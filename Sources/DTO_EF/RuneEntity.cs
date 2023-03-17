using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_EF
{
    public class RuneEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Familly { get; set; }

        public ImageEntity Image { get; set; }

        public ICollection<RunePageEntity> RunePages { get; set; } = new List<RunePageEntity>();
    }
}
