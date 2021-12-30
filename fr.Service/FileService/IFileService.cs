using fr.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace fr.Service.FileService
{
    public interface IFileService : IGeneratorService
    {
        Task<bool> CalcSvgFile(IFormFile file);
    }
}
