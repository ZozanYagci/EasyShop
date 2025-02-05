﻿using DTOs.DTOs.SubCategoryDtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISubCategoryService:IGenericService<SubCategory>
    {
        Task<List<GetSubCategoryByCategoryIdDto>> GetSubCategoryByCategoryIdAsync();
    }
}
