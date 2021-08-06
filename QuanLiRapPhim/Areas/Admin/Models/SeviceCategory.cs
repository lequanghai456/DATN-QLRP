using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class SeviceCategory
    {
        [Key]
        public int Id { get; set; }
        public int IdSevice { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }
        [ForeignKey("IdSevice")]
        public virtual Sevice Sevice { get; set; }
        public bool IsDelete { get; set; }
    }
}