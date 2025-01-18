using DTOs.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.FilterDtos
{
    public class ProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public List<ProductPriceDto> Prices { get; set; }
        public List<ProductAttributeDto> Attributes { get; set; }
    }
}
