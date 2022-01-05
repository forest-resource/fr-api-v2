using AutoMapper;
using fr.Core.Expressions;
using fr.Database.EntityFramework;
using fr.Database.Model.Entities.Plots;
using fr.Database.Model.Entities.Trees;
using fr.Service.Generic;
using fr.Service.Model.Plots;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fr.Service.PlotService
{
    internal class PlotService : GenericService<Plot, PlotModel>, IPlotService
    {
        public PlotService(IDesignTimeDbContextFactory dbContextFactory, IMapper mapper)
            : base(dbContextFactory, mapper)
        {
        }

        public override async Task<PlotModel> CreateAsync(Plot model)
        {
            var treeEntities = DbContext.Set<Tree>().AsNoTracking();
            var treeNameEntities = await treeEntities.Select(r => r.ScienceName).ToListAsync();

            var trees = model.PlotPoints
                .Select(r => r.Tree)
                .GroupBy(r => r.ScienceName)
                .Select(r => r.First());

            var notAvailableTreeNames = trees
                .Select(r => r.ScienceName)
                .GroupJoin(treeNameEntities, r => r, r => r, (s, r) => new { nameFromModel = s, namesFromDb = r })
                .Where(r => !r.namesFromDb.Any())
                .Select(r => r.nameFromModel)
                .ToList();

            var notAvailableTrees = trees.Where(r => notAvailableTreeNames.Any(t => t == r.ScienceName));

            await DbContext.AddRangeAsync(notAvailableTrees);

            return await base.CreateAsync(model);
        }

        public override async Task<IEnumerable<PlotModel>> GetManyAsync(ExpressionRule model)
        {
            var result = await GetMany<PlotModel>(model);
            return result.ToList();
        }
    }
}
