using System.ComponentModel.DataAnnotations;

namespace QuanLiRapPhim.Areas.Admin.Models
{
    public class CategoryMovie
    {
        [Key]
        public int Id { get; set; }
        public int PhimId { get; set; }
        public int CategoryId { get; set; }
        public Movie movie { get; set; }
        public Category category { get; set; }

    }
}