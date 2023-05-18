
using Advanced.NET6.EFCore.DB.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.NET6.DemoTest
{
    public class EFQueryTest
    {
        public static void Show()
        {
            #region 其他查询
            using (CustomerDbContext dbContext = new CustomerDbContext())
            {
                {
                    var ids = new int[] { 54, 55, 56, 57, 58, 59, 60, 61, 63 };
                    var list = dbContext.SysUsers.Where(u => 1 == 1 && !(ids.Contains(u.Id)));//in查询
                    foreach (var user in list)
                    {
                        Console.WriteLine(user.Name);
                    }
                }
                {
                    var list = from u in dbContext.SysUsers
                               where new int[] { 54, 55, 56, 57, 58, 59, 60, 61, 63 }.Contains(u.Id)
                               select u;

                    foreach (var user in list)
                    {
                        Console.WriteLine(user.Name);
                    }
                }
                {
                    var list = dbContext.SysUsers.Where(u => new int[] { 54, 55, 56, 57, 58, 59, 60, 61, 63 }.Contains(u.Id))
                                              .OrderBy(u => u.Id) //排序--升序
                                              .OrderByDescending(c=>c.Name)
                                              .Select(u => new //投影
                                              {
                                                  Name = u.Name,
                                                  Pwd = u.Password
                                              }).Skip(3).Take(5); //跳过三条  再获取5条
                    foreach (var user in list)
                    {
                        Console.WriteLine(user.Name);
                    }
                }
                {
                    var list = dbContext.SysUsers.Where(u => u.Name.StartsWith("Richard") && u.Name.EndsWith("Richard"))
                      .Where(u => u.Name.EndsWith("Richard"))
                      .Where(u => u.Name.Contains("Richard"))
                      .Where(u => u.Name.Length < 10)
                      .OrderBy(u => u.Id);

                    foreach (var user in list)
                    {
                        Console.WriteLine(user.Name);
                    }
                }
                {
                    var list = from u in dbContext.CompanyInfo
                               join c in dbContext.SysUsers on u.Id equals c.CompanyId
                               where new int[] { 1, 2, 3, 5, 7, 8, 9, 10, 11, 12, 14, 17 }.Contains(u.Id)
                               select new
                               {
                                   Name = u.CompanyName,
                                   UserName = c.Name,
                                   Address = c.Address
                               };
                    foreach (var user in list)
                    {
                        System.Console.WriteLine($"{user.Name}-{user.Address}");
                    }
                }
            }

            using (CustomerDbContext dbContext = new CustomerDbContext())
            {
                {
                    try
                    {
                        string sql = "Update dbo.SysUser Set Password='Ricahrd老师-小王子' WHERE Id=@Id";
                        SqlParameter parameter = new SqlParameter("@Id", 1);
                        int flag = dbContext.Database.ExecuteSqlRaw(sql, parameter);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            using (CustomerDbContext dbContext = new CustomerDbContext())
            {
                {
                    IDbContextTransaction trans = null;
                    try
                    {
                        trans = dbContext.Database.BeginTransaction();
                        string sql = "Update dbo.SysUser Set Password='Test00xx' WHERE Id=@Id";
                        SqlParameter parameter = new SqlParameter("@Id", 3843);
                        dbContext.Database.ExecuteSqlRaw(sql, parameter);

                        string sql1 = "Update dbo.SysUser Set Password='Test00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abcTest00abc' WHERE Id=@Id";
                        SqlParameter parameter1 = new SqlParameter("@Id", 3843);
                        dbContext.Database.ExecuteSqlRaw(sql1, parameter1);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (trans != null)
                            trans.Rollback();
                    }
                    finally
                    {
                        trans.Dispose();
                    }
                }
            }
             
            #endregion

        }
    }
}
