using fr.Database.Model.Entities.Icons;
using fr.Service.Interfaces;
using fr.Service.Model.Icons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fr.Service.IconService
{
    public interface IIconService : IGenericService<Icon, IconModel>, IGeneratorService
    {
        Task<IEnumerable<IconModel>> GetAvailableIconsAsync();
    }
}