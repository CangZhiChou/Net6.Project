using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Advanced.NET6.EFCore.DB.Models
{
    public partial class Commodity
    { 
        public int Id { get; set; }
         
        public long? ProductId { get; set; }
         
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Title不能为空")]
        [StringLength(500, ErrorMessage = "最长只能是500长度")]
        public string? Title { get; set; }
        public decimal? Price { get; set; }
         
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }
    }
}
