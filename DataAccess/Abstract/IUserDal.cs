using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal:IGenericRepository<AuthUser>
    {
        Task<AuthUser> GetByEmailAsync(string email);
    }
}
