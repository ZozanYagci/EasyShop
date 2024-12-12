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
    public class EfReviewDal:GenericRepository<Review, Context>, IReviewDal
    {
        public EfReviewDal(Context context) : base(context)
        {

        }
    }
}
