using Core.DataAccess.EntityFramework;
using DTOs.DTOs.SubCategoryDtos;
using Entities.Concrete;


namespace DataAccess.Abstract
{
    public interface ISubCategoryDal:IGenericRepository<SubCategory>
    {
        Task<List<GetSubCategoryByCategoryIdDto>> GetSubCategoryByCategoryIdAsync();
    }
}
