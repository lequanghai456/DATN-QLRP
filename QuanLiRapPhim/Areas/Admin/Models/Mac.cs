using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Mac
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên mac")]
        [Required(ErrorMessage = "Không được phép bỏ trống")]
        public String Title { get; set; }
        [Display(Name = "Độ tuổi")]
        [Required(ErrorMessage = "Không được phép bỏ trống")]
        public int Age { get; set; }
        [Display(Name = "Mô tả")]
        public String Describe { get; set; }
        public bool IsDelete { get; set; }
    }
}
