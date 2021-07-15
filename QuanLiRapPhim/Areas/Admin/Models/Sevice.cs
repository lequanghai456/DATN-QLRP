using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Sevice
    {
        [Key]
        public int Id{get;set;}
        [Display(Name="Tên dịch vụ")]
        [Required(ErrorMessage ="Tên dịch vụ không được để trống")]
        public String Name{get;set;}
        [Display(Name ="Chọn loại")]
        public bool IsFood { get; set; }
        public virtual ICollection<SeviceCategory> SeviceCategories{ get; set; }
        public bool IsDelete { get; set; }
    }
}