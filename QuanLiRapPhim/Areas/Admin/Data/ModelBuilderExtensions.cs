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
            //modelBuilder.Entity<Role>().HasData(
            //     new Role { Id = 1, Name = "admin" ,NormalizedName="ADMIN" },
            //     new Role { Id = 2, Name = "manager movie", NormalizedName="MANAGER MOVIE" },
            //     new Role { Id = 3, Name = "staff", NormalizedName="STAFF" }
            // ); 
            //modelBuilder.Entity<Staff>().HasData(
            //    new Staff
            //    {
            //        Id = 1,
            //        FullName = "Hồ Gia Bảo",
            //        UserName = "admin",
            //        NormalizedUserName="ADMIN",
            //        PasswordHash = "AQAAAAEAACcQAAAAEF9u1bOqWI0jVx9W90CBKSC4tesC72Ddrk3XnwUMBXBIZ5JdeuqgIPGi0UHCQxIUXQ==",
            //        Email = "0306181100@caothang.edu.vn",
            //        RoleId = 1,
            //        Img = "admin.img"
            //    }
            //);
            modelBuilder.Entity<Mac>().HasData(
                new Mac
                {
                    Id = 1,
                    Title = "P",
                    Age = 0,
                    Describe = "phim phù hợp với khán giả ở mọi lứa tuổi",
                },
                new Mac
                {
                    Id = 2,
                    Title = "C13",
                    Age = 13,
                    Describe = "phim cho khán giả từ 13 tuổi trở lên",
                },
                new Mac
                {
                    Id = 3,
                    Title = "C16",
                    Age = 16,
                    Describe = "phim cho khán giả từ 16 tuổi trở lên",
                },
                new Mac
                {
                    Id = 4,
                    Title = "C18",
                    Age = 18,
                    Describe = "phim cho khán giả từ 18 tuổi trở lên",
                }
            );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Title = "Detective",
                    
                },
                new Category
                {
                    Id = 2,
                    Title = "Adventure",

                }
            );
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Lật mặt",
                    MacId = 1,
                    Trailer = "1.mp4",
                    Poster = "1.jpg",
                    Describe = "Lý Hải trở lại với dòng phim hành động sở trường của mình. Bối cảnh hoành tráng với sự đầu tư nghiêm túc, siêu phẩm hành động Việt Lật Mặt 48h sẽ kể về một hành trình trốn chạy đầy kịch tính, nghẹt thở đến phút cuối cùng.",
                    Time = 110
                    
                },
                new Movie
                {
                    Id = 2,
                    Title = "Biệt đội báo thù",
                    MacId = 1,
                    Trailer = "2.mp4",
                    Poster = "2.jpg",
                    Describe = "Sau những sự kiện tàn khốc của Avengers: Infinity War (2018), vũ trụ đang dần tàn lụi. Với sự giúp đỡ của các đồng minh còn lại, các Avengers tập hợp một lần nữa để đảo ngược hành động của Thanos và khôi phục lại sự cân bằng cho vũ trụ.",
                    Time = 110

                },
                 new Movie
                 {
                     Id = 3,
                     Title = "BIỆT ĐỘI G.I. JOE: BÁO THÙ",
                     MacId = 1,
                     Trailer = "3.mp4",
                     Poster = "3.jpg",
                     Describe = "Đây là phần tiếp theo của bom tấn vô cùng ăn khách – “G.I. Joe: The Rise of Cobra”. Nội dung phần 2 của “G.I. Joe” bắt đầu khi những người lãnh đạo nước Mỹ bị tổ chức Cobra (kẻ thù không đội trời chung của đội đặc nhiệm G.I.Joe) kiểm soát và ra lệnh loại bỏ G.I.Joe. Toàn bộ nhóm đặc vụ bị gài bẫy và gần như bị xóa sổ. Những người còn sống của đội đặc nhiệm tìm đến sự giúp đỡ của người lãnh đạo G.I. Joe năm xưa – tướng Joe Colton để cùng nhau tìm nguyên nhân thực sự của mọi chuyện và tìm cách giải cứu nước Mỹ.",
                     Time = 110

                 }
            );
            modelBuilder.Entity<Sevice>().HasData(
                new Sevice
                {
                    Id = 1,
                    Name = "Bắp rang",
                    IsDelete = false,
                    Price=10000
                },
                new Sevice
                {
                    Id = 2,
                    Name = "CoCa",
                    IsDelete = false,
                    Price = 10000
                }, 
                new Sevice
                {
                    Id = 3,
                    Name = "Pepsi",
                    IsDelete = false,
                    Price = 10000
                }
                );
        }
    }
}
