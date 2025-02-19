using Microsoft.EntityFrameworkCore;
using AianBlazorPortfolioNet6.Components.Data;

namespace AianBlazorPortfolioNet6.Helpers
{
    public static class DataHelper
    {
        public static async Task ManageDataAsync(IServiceProvider serviceProvider)
        {
            // Here we assume your DbContext is TestimonialDbContext. 
            // Update the type if your context has a different name.
            var dbContext = serviceProvider.GetRequiredService<TestimonialDbContext>();
            // This applies any pending migrations (equivalent to Update-Database)
            await dbContext.Database.MigrateAsync();
        }
    }
}
