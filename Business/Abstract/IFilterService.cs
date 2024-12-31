using DTOs.DTOs.FilterAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFilterService
    {
        Task<List<ColorListDto>> GetColorListAsync();
        Task<List<SizeListDto>> GetSizeListAsync();
        Task<List<ComponentListDto>> GetComponentListAsync();
        Task<List<PriceRangeListDto>> GetPriceRangeListAsync();
    }
}
