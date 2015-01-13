using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Criteria
{
    public abstract class Criteria
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
