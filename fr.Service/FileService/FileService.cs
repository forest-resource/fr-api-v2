using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace fr.Service.FileService
{
    public class FileService : IFileService
    {
        public Task<bool> CalcSvgFile(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            using var rs = new StreamReader(stream);

            var svg = rs.ReadToEnd();

            return Task.FromResult(true);
        }
    }
}
