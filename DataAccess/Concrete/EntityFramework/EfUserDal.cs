using Core.DataAccess.GenericRepository;
using Core.Entities.Concrete.RequestModels;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal:GenericRepository<User, Context> , IUserDal
    {
        public EfUserDal(Context context) : base(context)
        {

        }
    }
}
