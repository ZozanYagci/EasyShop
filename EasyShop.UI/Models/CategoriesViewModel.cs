using DTOs.DTOs.ProductDtos;
using EasyShop.DTOs.DTOs.CategoryDtos;

namespace EasyShop.UI.Models
{
    public class CategoriesViewModel
    {
       public List<CategoryListDto> Categories { get; set; }

        public List<ProductStockDto> ProductStocks { get; set; }
    }
}
