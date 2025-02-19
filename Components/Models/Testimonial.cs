using System.ComponentModel.DataAnnotations;

namespace AianBlazorPortfolioNet6.Components.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool Approved { get; set; }
        public bool Featured { get; set; }
        public double Rating { get; set; }
    }

}