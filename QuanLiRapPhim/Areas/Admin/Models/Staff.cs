using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Staff : IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(255)")]
        public String FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public String Img { get; set; }
        public  int RoleId { get; set; }
        



    }
}
