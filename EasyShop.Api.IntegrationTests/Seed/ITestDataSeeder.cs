using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Api.IntegrationTests.Seed
{
    public interface ITestDataSeeder
    {
        void Seed(Context context);
    }
}
