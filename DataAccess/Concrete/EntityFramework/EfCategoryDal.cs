using Core.DataAccess.GenericRepository;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal:GenericRepository<Category, Context> , ICategoryDal
    {
        public EfCategoryDal(Context context) : base(context)
        {

        }
    }
}
