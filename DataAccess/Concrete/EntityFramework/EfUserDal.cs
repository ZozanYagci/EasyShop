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

        public async Task<List<OperationClaim>> GetClaim(AuthUser authUser)
        {
                var result = from operationClaim in _context.OperationClaims
                             join userOperationClaim in _context.AuthUserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.AuthUserId == authUser.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return await result.ToListAsync();

        }
    }
}
