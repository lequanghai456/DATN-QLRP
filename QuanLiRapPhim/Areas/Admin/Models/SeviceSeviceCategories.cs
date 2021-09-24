using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class SeviceSeviceCategories
    {
        [Key]
        public int Id { get; set; }
       
        public int IdSevice {get;set;}
        [ForeignKey("IdSevice")]
        public virtual Sevice Sevice { get; set; }
        public int IdSeviceCategory {get;set;}
        [ForeignKey("IdSeviceCategory")]
        public virtual SeviceCategory SeviceCategory { get; set; }
        public decimal Price {get;set;}
        public bool isDelete { get; set; }

    }
}
