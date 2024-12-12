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
    public class EfShoppingCartDal:GenericRepository<ShoppingCart, Context>, IShoppingCartDal
    {
        public EfShoppingCartDal(Context context) : base(context)
        {

        }
    }
}
