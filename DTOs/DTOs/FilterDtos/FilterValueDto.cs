using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.FilterDtos
{
    public class FilterValueDto
    {
        public string FilterName { get; set; }
        public List<string> Values { get; set; }
    }
}
