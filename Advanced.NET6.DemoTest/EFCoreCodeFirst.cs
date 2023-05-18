using Advanced.NET6.EFCore.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.NET6.DemoTest
{
    public class EFCoreCodeFirst
    {
        public static void Show()
        {
            using (CustomerDbContext context = new CustomerDbContext())
            { 
                context.Database.EnsureDeleted(); //删除数据库
                context.Database.EnsureCreated();//创建全新的数据库 
            }
        }
    }
}
