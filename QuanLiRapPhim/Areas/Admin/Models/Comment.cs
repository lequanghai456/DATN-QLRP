using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Comment
    {
        [Key]
        public int Id{ get; set; }
        public int? UserId{ get; set; }
        public String Content{ get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",ApplyFormatInEditMode =true)]
        public DateTime SubmittedDate{ get; set; }
        public int? MovieId{ get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
        public int Parent { get; set; }
        public bool IsDelete { get; set; }
    }
}