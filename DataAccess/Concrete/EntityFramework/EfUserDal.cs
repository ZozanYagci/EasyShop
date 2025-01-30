using Core.DataAccess.GenericRepository;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : GenericRepository<AuthUser, Context>, IUserDal
    {
        private readonly Context _context;
        public EfUserDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<AuthUser> GetByEmailAsync(string email)
        {
            return await _context.Set<AuthUser>().FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}
