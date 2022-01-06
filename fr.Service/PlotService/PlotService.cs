using AutoMapper;
using fr.Core.Exceptions;
using fr.Core.Expressions;
using fr.Database.EntityFramework;
using fr.Database.Model.Entities.Icons;
using fr.Database.Model.Entities.Plots;
using fr.Database.Model.Entities.Trees;
using fr.Service.Generic;
using fr.Service.Model.Plots;
using fr.Service.Model.Trees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fr.Service.PlotService
{
    internal class PlotService : GenericService<Plot, PlotModel>, IPlotService
    {
        public PlotService(IDesignTimeDbContextFactory dbContextFactory, IMapper mapper)
            : base(dbContextFactory, mapper)
        { }

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

            return await result.ToListAsync();
        }

        public override async Task<PlotModel> GetAsync(Guid key)
        {
            var plot = await Entities.FindAsync(key);

            if (plot == null)
            {
                throw new EntityNotFoundException($"Plot not found {key}");
            }

            var plotPointQuery = DbContext.Set<PlotPoint>().Where(r => r.PlotId == plot.Id);
            var currentTrees = DbContext.Set<Tree>()
                .Join(plotPointQuery, r => r.Id, r => r.TreeId, (tree, plotPoint) => new
                {
                    tree.Id,
                    tree.CommonName,
                    tree.ScienceName,
                    tree.Family,
                    IconData = tree.Icon.IconData
                });

            var treeDetails = DbContext.Set<TreeDetail>()
                .Join(currentTrees, r => r.TreeId, r => r.Id, (treeDetail, tree) => new
                {
                    treeDetail.TreeId,
                    treeDetail.Key,
                    treeDetail.Value
                })
                .ToList()
                .GroupBy(r => r.TreeId, (key, group) => new
                {
                    key,
                    group = group.Select(r => new TreeDetailModel
                    {
                        Key = r.Key,
                        Value = r.Value,
                    })
                }).ToDictionary(r => r.key, r => r.group.ToList());

            var result = new PlotModel
            {
                Id = plot.Id,
                Name = plot.PlotName,
                Title = plot.Title,
                Subtitle = plot.Subtitle,
                Description = plot.Description,
                IsCurrent = plot.IsCurrent,
                LastEdited = plot.UpdatedTime,
                By = plot.UpdatedBy,
                PlotPoints = plotPointQuery.Select(r => new PlotPointModel
                {
                    Id = r.Id,
                    Code = r.Code,
                    Sub = r.Sub,
                    X = r.X,
                    Y = r.Y,
                    Diameter = r.Diameter,
                    Height = r.Height,
                    TreeId = r.TreeId
                }).ToList(),
                Trees = currentTrees.Select(r => new TreeModel
                {
                    Id = r.Id,
                    CommonName = r.CommonName,
                    ScienceName = r.ScienceName,
                    Family = r.Family,
                    TreeDetails = treeDetails.ContainsKey(r.Id) ? treeDetails[r.Id] : Array.Empty<TreeDetailModel>(),
                    IconData = r.IconData
                }).ToList()
            };

            return result;
        }
    }
}
