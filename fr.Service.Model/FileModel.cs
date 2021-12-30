using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace fr.Service.Model
{
    public class FileModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
