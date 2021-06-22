using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Staff : IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(255)")]
        public String FullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public String Img { get; set; }
        public  int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        public bool IsDelete { get; set; }


    }
}
