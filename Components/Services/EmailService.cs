using AianBlazorPortfolioNet6.Components.Controller;

namespace AianBlazorPortfolioNet6.Components.Services
{
    public class EmailService
    {
        public Task<bool> SendEmailAsync(EmailRequest request)
        {
            return Task.FromResult(true);
        }
    }
}
