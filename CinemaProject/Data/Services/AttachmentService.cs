using CinemaProject.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace CinemaProject.Data.Services
{
    public interface IAttachmentService {
        public Attachment TurnFileIntoAttachment(IBrowserFile file);
        public Task<Attachment> UploadFile(IBrowserFile file);
        public Task<Boolean> DeleteFile(Attachment file);
        public Task<Attachment?> GetAttachmentById(int id);
    }

    public class AttachmentService(ApplicationDbContext context, ILogger<AttachmentService> logger) : IAttachmentService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger _logger = logger;
        //
        private class FilePath {
            private readonly string baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets");
            public string Uid {get;}
            public string FileInputDate {get;}
            public string FileName {get;} 
            public string FileDirectory {get;}
            public string FileFullPath {get;}

            public FilePath(IBrowserFile file) {
                this.Uid = Guid.NewGuid().ToString();
                this.FileInputDate = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                this.FileName = (this.FileInputDate + this.Uid + file.Name);
                this.FileDirectory = Path.Combine(baseDirectory, file.ContentType.Split("/")[0]);
                this.FileFullPath = Path.Combine(this.FileDirectory, this.FileName);
            }
        }

        public Attachment TurnFileIntoAttachment(IBrowserFile file) {
            // turns a file input into an Attachment object.
            // returns an attachment object with incomplete data (since it has not been uploaded.)
            //
            return new Attachment {
                Name = "",
                ContentType = "",
                URLPath = new FilePath(file).FileFullPath
            };
        }
        public async Task<Attachment> UploadFile(IBrowserFile file) {
            // Uploads a user input file into storage.
            // returns a full Attachment directly from the database.
            //
            
            var filePath = new FilePath(file);
            //
            // Saves file to wwwRoot directory.
            // Creates it if it doesnt exist
            Directory.CreateDirectory(filePath.FileDirectory);
            await using (var fileStream = new FileStream(filePath.FileFullPath, FileMode.Create)) {
                await file.OpenReadStream(1024 * 1024 * 26).CopyToAsync(fileStream);
            }
            //
            // Saves attachment to database.
            Attachment tempAttachment = new()
            {
                Name = filePath.FileName,
                URLPath = filePath.FileFullPath,
                ContentType = file.ContentType
            };

            var entityEntry = await _context.Attachments.AddAsync(tempAttachment);
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