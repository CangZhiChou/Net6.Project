using Advanced.NET6.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.NET6.Business.Services
{
    public class CommodityService : BaseService, ICommodityService
    {

        public CommodityService(DbContext context):base(context)
        { 
        
        }
    }
}
