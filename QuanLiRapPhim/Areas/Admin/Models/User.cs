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
        [Column(TypeName = "nvarchar(255)")]
        public String FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Img { get; set; }
        public bool ConfirmEmail { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Rate> Rate { get; set; }
        public bool IsDelete { get; set; }
       
    }
}
