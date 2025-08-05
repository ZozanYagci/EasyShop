using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Interfaces;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
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
        private readonly IFileService _fileService;
        private readonly IProductImageDal productImageDal;
        private readonly IMapper _mapper;
        private readonly Context dbContext;

        public ProductImageManager(IProductImageDal productImageDal, IMapper mapper, IFileService fileService, Context dbContext)
        {
            this.productImageDal = productImageDal;
            _mapper = mapper;
            _fileService = fileService;
            this.dbContext = dbContext;
        }

        public Task AddAsync(ProductImage entity)
        {
            throw new NotImplementedException();
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
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var image = await productImageDal.Get(i => i.Id == imageId);
                if (image == null)
                    return new ErrorResult(ProductImageMessages.ProductImageNotFound);

                //fiziksel dosya silinir
                var deleteResult = await _fileService.DeleteAsync(image.ImageUrl);
                if (!deleteResult)
                {
                    await transaction.RollbackAsync();
                    return new ErrorResult(ProductImageMessages.ProductImageDeleteFailed);
                }


                await productImageDal.DeleteAsync(image);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return new SuccessResult(ProductImageMessages.ProductImageDeleted);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ErrorResult($"Silme işlemi başarısız: {ex.Message}"); //TODO: logging
            }

        }

        public Task UpdateAsync(ProductImage entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<ProductImageDto>>> UploadImagesAsync(ProductImageUploadDto uploadDto)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var imageCount = await productImageDal.GetImageCountByProductId(uploadDto.ProductId);
                if (imageCount + uploadDto.Images.Count > 5) //db deki mevcut görseller + kullanıcının ekleyeceği görseller
                    return new ErrorDataResult<List<ProductImageDto>>("En fazla 5 görsel yüklenebilir.");


                var productImages = new List<ProductImage>();

                foreach (var image in uploadDto.Images)
                {
                    var filePath = await _fileService.UploadAsync(image, "products");

                    var productImage = new ProductImage
                    {
                        ProductId = uploadDto.ProductId,
                        ImageUrl = filePath,
                        CreatedAt = DateTime.UtcNow
                    };

                    await productImageDal.AddAsync(productImage);
                    productImages.Add(productImage);
                }

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                var imageDtos = _mapper.Map<List<ProductImageDto>>(productImages);
                return new SuccessDataResult<List<ProductImageDto>>(imageDtos, ProductImageMessages.ProductImagesUploaded);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ErrorDataResult<List<ProductImageDto>>($"Yükleme başarısız: {ex.Message}");
            }
        }
    }
}
