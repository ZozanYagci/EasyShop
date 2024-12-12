﻿using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal:IGenericRepository<User>
    {
    }
}
