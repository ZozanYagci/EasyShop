using Core.DataAccess.GenericRepository;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductImageDal : GenericRepository<ProductImage, Context>, IProductImageDal
    {
        private readonly Context dbContext;
        public EfProductImageDal(Context context) : base(context)
        {
            dbContext = context;
        }

        public async Task<int> GetImageCountByProductId(int productId)
        {
            return await dbContext.ProductImages.CountAsync(x => x.ProductId == productId);
        }
    }
}
