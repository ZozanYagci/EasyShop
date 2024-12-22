using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.ProductDtos
{
    public class ProductWithPricesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public List<ProductPriceDto> Prices { get; set; }
    }

    public class ProductPriceDto
    {
        public decimal Price { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
