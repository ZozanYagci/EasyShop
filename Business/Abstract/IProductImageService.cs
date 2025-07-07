using Core.Utilities.Results;
using DTOs.DTOs.ProductImageDtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductImageService : IGenericService<ProductImage>
    {
        Task<IDataResult<ProductImageDto>> AddImageAsync(ProductImageCreateDto imageDto);
        Task<IResult> RemoveImageAsync(int imageId);
        Task<IDataResult<List<ProductImageDto>>> GetImagesByProductIdAsync(int productId);
    }
}
