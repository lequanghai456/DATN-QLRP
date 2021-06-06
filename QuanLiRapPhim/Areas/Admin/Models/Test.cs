using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
    }
}
