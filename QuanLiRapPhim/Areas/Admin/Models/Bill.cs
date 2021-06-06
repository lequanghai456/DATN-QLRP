using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Decimal TotalPrice { get; set; }
        public bool IsDelete {get;set;}
    }
}