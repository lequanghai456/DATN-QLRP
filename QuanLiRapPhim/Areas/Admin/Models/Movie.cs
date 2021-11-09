using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên phim")]
        [Required(ErrorMessage = "Tên phim không được phép bỏ trống")]
        public string Title { get; set; }

        [Display(Name = "Mac")]
        public int? MacId { get; set; }
        [Display(Name = "Giới thiệu")]
        public String Trailer { get; set; }
        [Display(Name = "Ảnh phim")]
        public String Poster { get; set; }
        [Display(Name = "Mô tả")]
        public String Describe { get; set; }
        public int Status { get; set; }
        [Required(ErrorMessage = "Thời lượng không được bỏ trống")]
        [Display(Name = "Thời lượng")]
        [Range(45, int.MaxValue, ErrorMessage = "Thời lượng phim ít nhất 45 phút, nhiều nhất 3h")]
        public int Time { get; set; }
        public int TotalRating { get; set; }
        public int TotalReviewers { get; set; }
        [ForeignKey("MacId")]
        public virtual Mac Mac { get; set; }
        public virtual ICollection<Category> Lstcategories { get; set; }
        public virtual ICollection<ShowTime> LstShowTime { get; set; }
        public virtual ICollection<Comment> LstComment { get; set; }
        public virtual ICollection<User> RatedUsers { get; set; }
        public bool IsDelete { get; set; }
    }


}
