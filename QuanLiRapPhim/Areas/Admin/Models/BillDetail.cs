using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class BillDetail
    {
        [Key]
        public int Id{get;set;}
        public int? idSeviceSeviceCategories { get;set;}
        public String Name { get; set; }
        public int Amount{get;set;}
        public Decimal UnitPrice{get;set;}
        public int? BillId{get;set;}
        [ForeignKey("BillId")]
        public virtual Bill Bill { get; set; }
        [ForeignKey("idSeviceSeviceCategories")]
        public virtual SeviceSeviceCategories seviceSeviceCategories { get; set; }
    }
}
