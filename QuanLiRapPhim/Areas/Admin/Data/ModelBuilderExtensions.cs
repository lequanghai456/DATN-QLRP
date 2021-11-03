using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
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
            var passwordHasher = new PasswordHasher();
            modelBuilder.Entity<Role>().HasData(
                 new Role { Id = 1, Name = "admin", NormalizedName = "ADMIN" },
                 new Role { Id = 2, Name = "staff", NormalizedName = "STAFF" }
             );
            modelBuilder.Entity<Staff>().HasData(

                new Staff
                {
                    Id = 1,
                    FullName = "Hồ Gia Bảo",
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    PasswordHash = passwordHasher.HashPassword("abc123"),
                    Email = "0306181100@caothang.edu.vn",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    RoleId = 1,
                    Img = "admin.img"
                }, 
                new Staff
                {
                    Id = 2,
                    FullName = "Lê Quang Hải",
                    UserName = "manager1",
                    NormalizedUserName = "manager1",
                    PasswordHash = passwordHasher.HashPassword("abc123"),
                    Email = "0306181113@caothang.edu.vn",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    RoleId = 2,
                    Img = "manager1.img"
                }
            );
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
                    Title = "Trinh thám",

                },
                new Category
                {
                    Id = 2,
                    Title = "Phiêu lưu",

                },
                new Category
                {
                    Id = 3,
                    Title = "Hành động",

                },
                new Category
                {
                    Id = 4,
                    Title = "Hoạt hình",

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
                    Time = 110,
                                      
                },
                new Movie
                {
                    Id = 2,
                    Title = "Biệt đội báo thù",
                    MacId = 1,
                    Trailer = "2.mp4",
                    Poster = "2.jpg",
                    Describe = "Sau những sự kiện tàn khốc của Avengers: Infinity War (2018), vũ trụ đang dần tàn lụi. Với sự giúp đỡ của các đồng minh còn lại, các Avengers tập hợp một lần nữa để đảo ngược hành động của Thanos và khôi phục lại sự cân bằng cho vũ trụ.",
                    Time = 110,
                   
                },
                 new Movie
                 {
                     Id = 3,
                     Title = "BIỆT ĐỘI G.I. JOE: BÁO THÙ",
                     MacId = 1,
                     Trailer = "3.mp4",
                     Poster = "3.jpg",
                     Describe = "Đây là phần tiếp theo của bom tấn vô cùng ăn khách – “G.I. Joe: The Rise of Cobra”. Nội dung phần 2 của “G.I. Joe” bắt đầu khi những người lãnh đạo nước Mỹ bị tổ chức Cobra (kẻ thù không đội trời chung của đội đặc nhiệm G.I.Joe) kiểm soát và ra lệnh loại bỏ G.I.Joe. Toàn bộ nhóm đặc vụ bị gài bẫy và gần như bị xóa sổ. Những người còn sống của đội đặc nhiệm tìm đến sự giúp đỡ của người lãnh đạo G.I. Joe năm xưa – tướng Joe Colton để cùng nhau tìm nguyên nhân thực sự của mọi chuyện và tìm cách giải cứu nước Mỹ.",
                     Time = 110,
                 },
                 new Movie
                 {
                     Id = 4,
                     Title = "BẠCH XÀ 2: THANH XÀ KIẾP KHỞI",
                     MacId = 1,
                     Trailer = "4.mp4",
                     Poster = "4.jpg",
                     Describe = "Cuối thời Nam Tống, Tiểu Bạch vì muốn cứu Hứa Tiên mà dâng nước dìm Kim Sơn tự, cuối cùng bị Pháp Hải nhốt dưới tháp Lôi Phong. Tiểu Thanh bất ngờ bị Pháp Hải đánh rơi vào ảo cảnh của Tu La thành quỷ dị. Mấy lần nguy cấp, Tiểu Thanh được một thiếu niên thần bí cứu, Tiểu Thanh mang theo chấp niệm cứu Tiểu Bạch ra ngoài mà trải qua kiếp nạn rồi trưởng thành, cùng thiếu niên thần bí đi tìm biện pháp.",
                     Time = 132,
                 },
                 new Movie
                 {
                     Id = 5,
                     Title = "LÃNG KHÁCH KENSHIN: KHỞI ĐẦU",
                     MacId = 1,
                     Trailer = "5.mp4",
                     Poster = "5.jpg",
                     Describe = "Trước khi Himura Kenshin gặp Kaoru, anh ta là một sát thủ đáng sợ được gọi là Hitokiri Battousai. 'Rurouni Kenshin: The Beginning' kể câu chuyện về một Kenshin trẻ tuổi, người trở thành sát thủ số một cho Ishin Shishi (sau này trở thành 'thời Minh Trị'), người đã chiến đấu chống lại Mạc phủ trong những ngày cuối cùng của thời đại Tokugawa, và làm thế nào anh ấy có dấu 'X' nổi tiếng trên má trái của mình. Trong những ngày đầu của HimuraKenshin trong vai Hitokiri Battousai, một ngày nọ, anh gặp Yukishiro Tomoe, một thiếu nữ xinh đẹp, có vẻ ngoài thanh thoát nhưng luôn mang một khuôn mặt buồn bã. Battousai và Tomoe yêu nhau nhưng ít ai biết rằng, Tomoe mang trong lòng một gánh nặng to lớn sẽ thay đổi cuộc đời của Himura Kenshin mãi mãi.",
                     Time = 140,
                 }
            );

            modelBuilder.Entity<Sevice>().HasData(
                 new Sevice
                 {
                     Id = 1,
                     Name = "Bắp rang",
                     IsFood = true,

                 },
                 new Sevice
                 {
                     Id = 2,
                     Name = "CoCa",
                     IsFood = false,

                 },
                 new Sevice
                 {
                     Id = 3,
                     Name = "Pepsi",
                     IsFood = false,

                 }
            );

            modelBuilder.Entity<SeviceCategory>().HasData(
                new SeviceCategory
                {
                    Id = 1,
                    Name = "Big",

                },
                new SeviceCategory
                {
                    Id = 2,
                    Name = "Small",

                },
                new SeviceCategory
                {
                    Id = 3,
                    Name = "Medium",

                }
                );
            modelBuilder.Entity<SeviceSeviceCategories>().HasData(
                new SeviceSeviceCategories
                {
                    Id = 1,
                    IdSevice = 1,
                    IdSeviceCategory = 1,
                    Price=30000
                    
                },
                new SeviceSeviceCategories
                {
                    Id = 2,
                    IdSevice = 1,
                    IdSeviceCategory = 2,
                    Price = 25000
                }, new SeviceSeviceCategories
                {
                    Id = 3,
                    IdSevice = 1,
                    IdSeviceCategory = 3,
                    Price = 10000
                }, new SeviceSeviceCategories
                {
                    Id = 4,
                    IdSevice = 2,
                    IdSeviceCategory = 1,
                    Price = 10000
                }, new SeviceSeviceCategories
                {
                    Id = 5,
                    IdSevice = 2,
                    IdSeviceCategory = 2,
                    Price = 30000
                }, new SeviceSeviceCategories
                {
                    Id = 6,
                    IdSevice = 3,
                    IdSeviceCategory = 1,
                    Price = 10000
                }, new SeviceSeviceCategories
                {
                    Id = 7,
                    IdSevice = 3,
                    IdSeviceCategory = 2,
                    Price = 30000
                }
                );
        }
    }
}
