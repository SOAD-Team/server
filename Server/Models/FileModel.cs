using Microsoft.AspNetCore.Http;

namespace Server.Models
{
    public class FileModel
    {
        public FileModel(string v, IFormFile image)
        {
            this.FileName = v;
            this.FormFile = image;
        }

        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
