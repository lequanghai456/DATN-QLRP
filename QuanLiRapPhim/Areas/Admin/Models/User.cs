using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class User : IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Rate> Rate { get; set; }

    }
}
