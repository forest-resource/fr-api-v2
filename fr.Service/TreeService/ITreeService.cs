using fr.Database.Model.Entities.Trees;
using fr.Service.Interfaces;
using fr.Service.Model.Trees;

namespace fr.Service.TreeService
{
    public interface ITreeService : IGenericService<Tree, TreeModel>, IGeneratorService
    {
    }
}
