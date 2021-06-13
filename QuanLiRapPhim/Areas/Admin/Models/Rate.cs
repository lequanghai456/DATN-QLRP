using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Rate
    {
        [Key]
        public int Id{get;set;}
        public String UserName{get;set;}
        public bool IsDelete { get; set; }

    }
}