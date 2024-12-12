using DTOs.DTOs.SubCategoryDtos;
using EasyShop.DTOs.DTOs.CategoryDtos;

namespace EasyShop.UI.Models
{
    public class NavbarViewModel
    {
        public List<CategoryListDto> Categories { get; set; }
        public List<GetSubCategoryByCategoryIdDto> SubCategories { get; set; }
    }
}
