using Core.DataAccess.EntityFramework;
using Entities.Concrete;


namespace DataAccess.Abstract
{
    public interface IProductImageDal : IGenericRepository<ProductImage>
    {
        Task<int> GetImageCountByProductId(int productId);
    }
}
