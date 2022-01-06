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
            var plotQuery = model.All(r => r.Value == null)
                ? Entities.AsNoTracking()
                : Entities.AsNoTracking().Where(model.Build<Plot>());
            var plotPointQuery = DbContext.Set<PlotPoint>().AsNoTracking()
                .Join(plotQuery, r => r.PlotId, r => r.Id, (point, plot) => point);
            var treeQuery = DbContext.Set<Tree>().AsNoTracking()
                .Join(plotPointQuery, r => r.Id, r => r.TreeId, (tree, point) => tree)
                .Distinct();

            var result = plotQuery.Select(plot => new PlotModel
            {
                Id = plot.Id,
                Name = plot.PlotName,
                Description = plot.Description,
                IsCurrent = plot.IsCurrent,
                Subtitle = plot.Subtitle,
                Title = plot.Title,
                LastEdited = plot.UpdatedTime,
                By = plot.UpdatedBy
            });

            return result.ToList();
        }
    }
}
