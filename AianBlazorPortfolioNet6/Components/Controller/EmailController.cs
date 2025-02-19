
using AianBlazorPortfolioNet6.Components.Services;
using Microsoft.AspNetCore.Mvc;

namespace AianBlazorPortfolioNet6.Components.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;
        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            bool result = await _emailService.SendEmailAsync(request);
            if (result)
                return Ok(new { message = "Email sent" });
            else
                return StatusCode(500, new { message = "Failed to send email" });
        }
    }

    public class EmailRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

}
