﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.NET6.Business.Interfaces
{
    public interface ICompanyService:IBaseService
    {
        public void DeleteCompanyAndUser();
    }
}
