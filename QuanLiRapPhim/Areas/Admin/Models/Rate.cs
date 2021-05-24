using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Rate
    {
        [Key]
        public int Id{get;set;}
        public String UserName{get;set;}
        public int? MovieId{get;set;}
        public int Star{get;set;}
    }
}