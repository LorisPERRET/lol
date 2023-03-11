using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_API.Pagination
{
    public class Page<T>
    {
        public int NbItem { get; set; }
        public int Offset { get; set; }
        public T Items { get; set; }

        public Page(int nbItem, int offset, T items)
        {
            NbItem = nbItem;
            Offset = offset;   
            Items = items;
        }
    }
}
