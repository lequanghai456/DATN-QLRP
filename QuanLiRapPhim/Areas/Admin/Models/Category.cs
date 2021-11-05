using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên thể loại")]
        [Required(ErrorMessage ="Không được phép bỏ trống")]
        [Remote(action: "VerifyTitle", controller: "Categories",areaName: "Admin", ErrorMessage = "Thể loại đẫ tồn tại")]
        public string Title { get; set; }
        public ICollection<Movie> lstMovie { get; set; }
        public bool IsDelete { get; set; }
    }
}
