using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public int Status { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        public bool IsDelete { get; set; }

    }
}