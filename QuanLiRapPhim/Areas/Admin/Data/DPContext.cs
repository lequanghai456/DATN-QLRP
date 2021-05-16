using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLiRapPhim.Areas.Admin.Models;
namespace QuanLiRapPhim.Areas.Admin.Data
{
    public class DPContext : DbContext
    {
        public DPContext(DbContextOptions<DPContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
