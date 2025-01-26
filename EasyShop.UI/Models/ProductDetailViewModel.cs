using DTOs.DTOs.ProductDtos;

namespace EasyShop.UI.Models
{
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public List<ProductDetailsDto>? ProductDetails { get; set; }
        public List<string> Paths { get; set; } = new List<string>();
    }
}
