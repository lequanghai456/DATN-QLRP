using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Mac
    {
        [Key]
        public int Id { get; set; }
        public String Title { get; set; }
        public int Age { get; set; }
        public String Describe { get; set; }
        public bool IsDelete { get; set; }
    }
}
