using Core.DataAccess.EntityFramework;
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
    public class EfOrderDetailDal: GenericRepository<OrderDetail, Context>, IOrderDetailDal
    {

        public EfOrderDetailDal(Context context) : base(context)
        {

        }
    }
}
