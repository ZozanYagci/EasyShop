using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.FilterDtos
{
    public class FilterRequestDto
    {
        public List<string> Colors { get; set; }
        public List<string> Components { get; set; }
        public List<string> Sizes { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
