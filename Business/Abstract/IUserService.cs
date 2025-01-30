using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService:IGenericService<AuthUser>
    {
        Task<string> RegisterAsync(AuthUser user, string password);
        Task<AccessToken> LoginAsync(string email, string password);
    }
}
