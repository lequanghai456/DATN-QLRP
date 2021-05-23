using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public String Name{get; set;}
        public int Row{get; set;}
        public int Col{get; set;}
        public int? IdStaftManager{get; set;}
        [ForeignKey("IdStaftManager")]
        public virtual Staff Staff { get; set; }
        public virtual ICollection<ShowTime> LstShowTime { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        
    }
}
