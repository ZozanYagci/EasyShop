using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.SubCategoryDtos
{
    public class GetSubCategoryByCategoryIdDto
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; } 
    }
}
