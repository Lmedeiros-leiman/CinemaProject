using CinemaProject.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace CinemaProject.Data.Services
{
    public interface IAttachmentService
    {
        public Task<Attachment> UploadFile(IBrowserFile file);
        public Task<Boolean> DeleteFile(Attachment file);
        public Task<Attachment?> GetAttachmentById(int id);
    }

    public class AttachmentService(ApplicationDbContext context, ILogger<AttachmentService> logger) : IAttachmentService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger _logger = logger;
        private readonly string baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets");

        public async Task<Attachment> UploadFile(IBrowserFile file)
        {

            string uniqueIdentifier = Guid.NewGuid().ToString();
            string fileInputDate = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            string fileName = (fileInputDate + uniqueIdentifier + file.Name);
            //
            string fileDirectory = Path.Combine(baseDirectory, file.ContentType.Split("/")[0]);
            string filePath = Path.Combine(fileDirectory, fileName);

            //
            // Saves file to wwwRoot directory.
            Directory.CreateDirectory(fileDirectory);
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.OpenReadStream(1024 * 1024 * 26).CopyToAsync(fileStream);
            }

            //
            // Saves attachment to database.
            Attachment tempObject = new()
            {
                Name = fileName,
                URLPath = $"/{file.ContentType.Split("/")[0]}/{fileName}",
                ContentType = file.ContentType
            };
            var entityEntry = await _context.Attachments.AddAsync(tempObject);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<Boolean> DeleteFile(Attachment movieAttachment)

        {

            var entry = await _context.Movies.FindAsync(movieAttachment.Id);
            if (entry == null) { return false;}
            // deletes fisical file
            string filePath = Path.Combine(baseDirectory,movieAttachment.URLPath);

            if (File.Exists(filePath) ) 
            {
                File.Delete(filePath);
            }

            //
            _context.Remove(entry);
            await _context.SaveChangesAsync();
            return true;
        }
    
        public async Task<Attachment?> GetAttachmentById(int id) {
            return await _context.Attachments.FindAsync(id);
        }
    }
}