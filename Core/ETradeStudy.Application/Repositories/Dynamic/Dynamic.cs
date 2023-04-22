using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Repositories.Dynamic
{
    public class Dynamic
    {
        public List<Sort>? Sort { get; set; }
        public List<Filter>? Filter { get; set; }

        public Dynamic()
        {
        }

    }
}
