using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Sevice
    {
        [Key]
        public int Id{get;set;}
        public String Name{get;set;}
        public Decimal Price{get;set;}
        public bool IsDelete { get; set; }
    }
}