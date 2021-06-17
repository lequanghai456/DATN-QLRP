using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLiRapPhim.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                 new Role { Id = 1, Name = "admin" },
                 new Role { Id = 2, Name = "manager movie" },
                 new Role { Id = 3, Name = "staff" }
             ); 
            modelBuilder.Entity<Staff>().HasData(
                new Staff
                {
                    Id = 1,
                    FullName = "Hồ Gia Bảo",
                    UserName = "admin",
                    PasswordHash = "AQAAAAEAACcQAAAAEF9u1bOqWI0jVx9W90CBKSC4tesC72Ddrk3XnwUMBXBIZ5JdeuqgIPGi0UHCQxIUXQ==",
                    Email = "0306181100@caothang.edu.vn",
                    RoleId = 1,
                    Img = "admin.img"
                }
            );
           
        }
    }
}
