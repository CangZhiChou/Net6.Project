using Advanced.NET6.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.NET6.Business.Services
{
    public class CompanyService : BaseService, ICompanyService
    {

        public CompanyService(DbContext context) : base(context)
        {

        }

        public void DeleteCompanyAndUser()
        {
            
        }
    }
}
