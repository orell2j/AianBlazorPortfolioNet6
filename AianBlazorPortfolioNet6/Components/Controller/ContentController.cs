﻿using AianBlazorPortfolioNet6.Components.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using AianBlazorPortfolioNet6.Components.Models;

namespace AianBlazorPortfolioNet6.Components.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly TestimonialDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public ContentController(TestimonialDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }

        // GET api/content
        [HttpGet]
        public async Task<ActionResult<SiteContent>> GetContent()
        {
            var content = await _dbContext.SiteContents.FirstOrDefaultAsync();
            if (content == null)
            {
                content = new SiteContent(); // You can set default values if needed.
                _dbContext.SiteContents.Add(content);
                await _dbContext.SaveChangesAsync();
            }
            return content;
        }


        // POST api/content/update
        [HttpPost("update")]
        public async Task<IActionResult> UpdateContent([FromBody] SiteContent updatedContent)
        {
            if (updatedContent == null)
            {
                return BadRequest();
            }

            var content = await _dbContext.SiteContents.FirstOrDefaultAsync();
            if (content == null)
            {
                _dbContext.SiteContents.Add(updatedContent);
            }
            else
            {
                content.AboutTextEnglish = updatedContent.AboutTextEnglish;
                content.AboutTextFrench = updatedContent.AboutTextFrench;
                content.AboutImageUrl = updatedContent.AboutImageUrl;
                content.WorksContentEnglish = updatedContent.WorksContentEnglish;
                content.WorksContentFrench = updatedContent.WorksContentFrench;
                content.SkillsContentEnglish = updatedContent.SkillsContentEnglish;
                content.SkillsContentFrench = updatedContent.SkillsContentFrench;
                content.ContactEmail = updatedContent.ContactEmail;
                content.ContactPhone = updatedContent.ContactPhone;
                content.GithubUrl = updatedContent.GithubUrl;
                content.LinkedInUrl = updatedContent.LinkedInUrl;
                content.CVFileFrenchUrl = updatedContent.CVFileFrenchUrl;
                content.CVFileEnglishUrl = updatedContent.CVFileEnglishUrl;
            }

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        // POST api/content/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided.");

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = $"/uploads/{file.FileName}";
            return Ok(new { fileUrl });
        }
    }
}
