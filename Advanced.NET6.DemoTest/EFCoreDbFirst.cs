using Advanced.NET6.EFCore.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.NET6.DemoTest
{
    public class EFCoreDbFirst
    {
        public static void Show()
        {
            using (CustomerDbContext context = new CustomerDbContext())
            {
                var add = new Commodity()
                {
                    CategoryId = 1,
                    ImageUrl = "Test",
                    Price = 343,
                    ProductId = 1,
                    Title = "Test",
                    Url = "Url"
                };
                context.Add<Commodity>(add);
                context.SaveChanges();

                Commodity commodity = context.Commodities.OrderByDescending(c => c.Id).FirstOrDefault();



                commodity.Title = "Richard";
                context.SaveChanges();


                context.Remove<Commodity>(commodity);
                context.SaveChanges();

            }
        }
    }
}
