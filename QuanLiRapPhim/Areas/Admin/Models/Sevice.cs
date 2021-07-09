using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Sevice
    {
        [Key]
        public int Id{get;set;}
        public String Name{get;set;}
        public bool IsFood { get; set; }
        public virtual ICollection<SeviceCategory> SeviceCategories{ get; set; }
        public bool IsDelete { get; set; }
    }
}