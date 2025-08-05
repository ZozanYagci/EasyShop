using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.ProductImageDtos
{
    public class ProductImageUploadDto
    {
        public int ProductId { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
