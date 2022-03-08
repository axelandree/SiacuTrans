using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.Helpers.Transac.Service
{
    public class HeaderAttribute : Attribute
    {
        public HeaderAttribute()
        {
            Order = -1;
        }
        public string Title { get; set; }
        public int Order { get; set; }
        public string Group { get; set; }
    }
}
