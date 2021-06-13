using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Decimal TotalPrice { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public bool IsDelete {get;set;}
    }
}