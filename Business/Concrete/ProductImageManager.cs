using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DTOs.DTOs.ProductImageDtos;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductImageManager : IProductImageService
    {

        private readonly IProductImageDal productImageDal;
        private readonly IMapper _mapper;

        public ProductImageManager(IProductImageDal productImageDal, IMapper mapper)
        {
            this.productImageDal = productImageDal;
            _mapper = mapper;
        }

        public Task AddAsync(ProductImage entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<ProductImageDto>> AddImageAsync(ProductImageCreateDto imageDto)
        {
            var productImage = _mapper.Map<ProductImage>(imageDto);
            productImage.CreatedAt = DateTime.Now;

            await productImageDal.AddAsync(productImage);
            var resultDto = _mapper.Map<ProductImageDto>(productImage);
            return new SuccessDataResult<ProductImageDto>(resultDto, ProductImageMessages.ProductImageAdded);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductImage>> GetAllAsync(bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<ProductImage> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<ProductImageDto>>> GetImagesByProductIdAsync(int productId)
        {
            var imagesQuery = productImageDal.Get(i => i.ProductId == productId, true);
            var images = await imagesQuery.ToListAsync();

            var imageDtos = _mapper.Map<List<ProductImageDto>>(images);
            return new SuccessDataResult<List<ProductImageDto>>(imageDtos, ProductImageMessages.ImageListed);
        }

        public async Task<IResult> RemoveImageAsync(int imageId)
        {
            var image = await productImageDal.Get(i => i.Id == imageId);
            if (image == null)
                return new ErrorResult(ProductImageMessages.ProductImageNotFound);

            await productImageDal.DeleteAsync(image);
            return new SuccessResult(ProductImageMessages.ProductImageDeleted);
        }

        public Task UpdateAsync(ProductImage entity)
        {
            throw new NotImplementedException();
        }
    }
}
