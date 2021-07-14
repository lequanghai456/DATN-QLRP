using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Ticket
    {
        [Key]
        public int Id{ get; set; }
        public int? SeatId{ get; set; }
        public int? ShowTimeId{ get; set; }
        public String Name { get; set; }
        public String Username { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Decimal Price { get; set; }
        [ForeignKey("SeatId")]
        public virtual Seat Seat { get; set; }
        [ForeignKey("ShowTimeId")]
        public virtual ShowTime ShowTime { get; set; }
        public bool IsDelete { get; set; }
        public bool Status { get; set; }
    }
}
