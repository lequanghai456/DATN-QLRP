using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Sevice
    {
        [Key]
        public int Id{get;set;}
        [Display(Name = "Tên dịch vụ")]
        [Required(ErrorMessage = "Không được phép bỏ trống")]
        public String Name{get;set;}
        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Không được phép bỏ trống")]
        public Decimal Price{get;set;}
        public bool IsDelete { get; set; }
    }
}