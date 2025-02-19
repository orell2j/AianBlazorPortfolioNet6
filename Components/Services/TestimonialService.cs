using AianBlazorPortfolioNet6.Components.Data;
using AianBlazorPortfolioNet6.Components.Models;

namespace AianBlazorPortfolioNet6.Components.Services
{
    public class TestimonialService
    {
        private readonly TestimonialDbContext _context;

        public TestimonialService(TestimonialDbContext context)
        {
            _context = context;
        }

        public async Task<Testimonial> AddTestimonialAsync(Testimonial testimonial)
        {
            testimonial.CreatedOn = DateTime.UtcNow;
            testimonial.Approved = false;
            _context.Testimonials.Add(testimonial);
            await _context.SaveChangesAsync();
            return testimonial;
        }
    }

}
