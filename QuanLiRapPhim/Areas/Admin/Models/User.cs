using Castle.MicroKernel.SubSystems.Conversion;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class User : IdentityUser<int>
    {
        [Display(Name = "Tên đầy đủ")]
        [Required(ErrorMessage = "Không được phép bỏ trống")]
        [Column(TypeName = "nvarchar(255)")]
        public String FullName { get; set; }
        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Không được phép bỏ trống")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Ảnh")]
        public String Img { get; set; }
        public bool ConfirmEmail { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Movie> MoviesRated { get; set; }
        public bool IsDelete { get; set; }
       
    }
}
