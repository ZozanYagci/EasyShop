using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.ProductDtos
{
    public class ProductStockDto
    {
        public string CategoryName { get; set; }
        public int StockQuantity { get; set; }

        public int SubCategoryId { get;set; }

    }
}
