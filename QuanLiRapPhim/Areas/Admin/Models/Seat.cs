using System.Collections;
using System.Collections.Generic;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Seat : Device
    {
        public int X{get;set;}
        public int Y{get;set;}
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}