using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class PaginationDto
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
