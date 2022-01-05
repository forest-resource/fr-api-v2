using fr.Service.Interfaces;
using fr.Service.Model.Plots;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fr.Service.FileService
{
    public interface IFileService : IGeneratorService
    {
        IFormFile File { get; set; }
        Task<bool> CalcSvgFile(IFormFile file);

        Task<IEnumerable<PlotPointFromFileModel>> ReadPlotPointsAsync();
    }
}
