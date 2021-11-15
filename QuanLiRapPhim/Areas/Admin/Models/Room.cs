using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên phòng")]
        public String Name{get; set;}
        [Display(Name = "Số lượng ghế ngang")]
        [Range(0,15, ErrorMessage = "Số lượng tối thiểu là 0, lớn nhất là 15")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        [RegularExpression("[0-9]*$", ErrorMessage = "Vui lòng nhập số nguyên")]
        public int Row{get; set;}
        [Display(Name = "Số lượng ghế dọc")]
        [Range(0, 15, ErrorMessage = "Số lượng tối thiểu là 0, lớn nhất là 15")]
        [RegularExpression("[0-9]*$", ErrorMessage = "Vui lòng nhập số nguyên")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int Col{get; set;}
        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        [RegularExpression("[0-9]*$",ErrorMessage ="Vui lòng nhập số nguyên")]
        public Decimal Price { get; set; }
        public virtual ICollection<ShowTime> LstShowTime { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
        public bool IsDelete { get; set; }

    }
}
