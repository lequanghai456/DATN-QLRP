﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int? MacId { get; set; }
        public String Trailer { get; set; } 
        public String Poster { get; set; }
        public String Describe { get; set; }
        public int Status { get; set; }
        public int Time { get; set; }
        public int TotalRating { get; set; }
        public int TotalReviewers { get; set; }
        [ForeignKey("MacId")]
        public virtual Mac Mac { get; set; }
        public virtual ICollection<Category> Lstcategories { get; set; }
        public virtual ICollection<ShowTime> LstShowTime { get; set; }
        public virtual ICollection<Comment> LstComment { get; set; }
        public bool IsDelete { get; set; }
    }


}
