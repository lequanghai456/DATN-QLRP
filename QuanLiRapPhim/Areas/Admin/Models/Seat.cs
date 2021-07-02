using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }
        public string X{get;set;}
        public int Y{get;set;}
        public decimal ExtraPrice { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
        public bool IsDelete { get; set; }
    }
}