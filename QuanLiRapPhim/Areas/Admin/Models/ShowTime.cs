using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class ShowTime
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateTime { get; set; }
        public int? MovieId { get; set; }
        public int? RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public bool IsDelete { get; set; }
    }
}
