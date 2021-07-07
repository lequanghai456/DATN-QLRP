using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Ngày lập")]
        public DateTime Date { get; set; }
        [Display(Name = "Tổng giá")]
        public Decimal TotalPrice { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public bool IsDelete {get;set;}
        public bool Status { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }

    }
}