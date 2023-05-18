using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced.NET6.EFCore.DB.Models
{
    [Table("Company")]
    public partial class CompanyInfo
    {
        public CompanyInfo()
        {
            SysUsers = new HashSet<SysUser>();
        }

        public int Id { get; set; }

        [Column("Name")]
        public string? CompanyName { get; set; }
        public DateTime? CreateTime { get; set; }
        public int CreatorId { get; set; }
        public int? LastModifierId { get; set; }
        public DateTime? LastModifyTime { get; set; }

        public virtual ICollection<SysUser> SysUsers { get; set; }
    }
}
