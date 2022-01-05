using fr.Database.Model.Entities.Plots;
using fr.Service.Interfaces;
using fr.Service.Model.Plots;

namespace fr.Service.PlotService
{
    public interface IPlotService : IGenericService<Plot, PlotModel>, IGeneratorService
    {
    }
}
