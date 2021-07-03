using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLiRapPhim.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Data
{
    public class IdentityContext : IdentityDbContext<Staff,Role,int>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();       
        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Mac> Macs { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public DbSet<Sevice> Sevices { get; set; }
        public DbSet<ShowTime> ShowTimes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Test> Test { get; set; }


    }
}
