using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.ProductDtos
{
    public class ProductListDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
