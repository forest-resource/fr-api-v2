using AutoMapper;
using fr.Core.Exceptions;
using fr.Core.Expressions;
using fr.Database.EntityFramework;
using fr.Database.Model.Entities.Icons;
using fr.Database.Model.Entities.Plots;
using fr.Database.Model.Entities.Trees;
using fr.Service.Generic;
using fr.Service.IconService;
using fr.Service.Model.Plots;
using fr.Service.Model.Trees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fr.Service.PlotService
{
    internal class PlotService : GenericService<Plot, PlotModel>, IPlotService
    {
        private readonly IIconService iconService;
        public PlotService(
            IDesignTimeDbContextFactory dbContextFactory,
            IMapper mapper,
            IIconService iconService
        ) : base(dbContextFactory, mapper)
        {
            this.iconService = iconService;
        }

        public override async Task<PlotModel> CreateAsync(Plot model)
        {
            var modelTrees = model.PlotPoints.Select(r => r.Tree).DistinctBy(r => r.ScienceName).ToList();
            var trees = DbContext.Set<Tree>().AsNoTracking()
                .Where(r => modelTrees.Select(r => r.ScienceName).Contains(r.ScienceName))
                .ToList();
            var availableIcons = (await iconService.GetAvailableIconsAsync()).ToList();
            var currentIconIndex = 0;

            foreach (var tree in modelTrees)
            {
                var treeEntity = trees.FirstOrDefault(r => r.ScienceName == tree.ScienceName);
                if (treeEntity != null)
                {
                    if (treeEntity.IconId.GetValueOrDefault() == Guid.Empty && currentIconIndex < availableIcons.Count)
                    {
                        treeEntity.IconId = availableIcons[currentIconIndex++].Id;
                        var treeEntry = DbContext.Update(treeEntity);
                        UpdatePlotPoint(model.PlotPoints, treeEntry.Entity);
                    }
                    else
                    {
                        UpdatePlotPoint(model.PlotPoints, treeEntity);
                    }
                }
                else
                {
                    treeEntity = tree;
                    if (treeEntity.IconId.GetValueOrDefault() == Guid.Empty && currentIconIndex < availableIcons.Count)
                    {
                        treeEntity.IconId = availableIcons[currentIconIndex++].Id;
                        var treeEntry = await DbContext.AddAsync(treeEntity);
                        UpdatePlotPoint(model.PlotPoints, treeEntry.Entity);
                    }
                    else
                    {
                        UpdatePlotPoint(model.PlotPoints, treeEntity);
                    }
                }

            }

            var result = await Entities.AddAsync(model);
            await DbContext.SaveChangesAsync();

            foreach (var point in result.Entity.PlotPoints)
            {
                if (point.Tree == null)
                {
                    point.Tree = trees.FirstOrDefault(tree => tree.Id == point.TreeId);
                }
            }

            return _mapper.Map<Plot, PlotModel>(result.Entity);
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
                    tree.Icon.IconData
                });

            var treeDetails = DbContext.Set<TreeDetail>()
                .Join(currentTrees, r => r.TreeId, r => r.Id, (treeDetail, tree) => new
                {
                    treeDetail.TreeId,
                    treeDetail.Key,
                    treeDetail.Value
                })
                .GroupBy(r => r.TreeId, (key, group) => new
                {
                    key,
                    group = group.Select(r => new TreeDetailModel
                    {
                        Key = r.Key,
                        Value = r.Value,
                    })
                }).ToDictionary(r => r.key, r => r.group.ToList());

            var treeModels = (await currentTrees.ToListAsync())
                .DistinctBy(r => r.ScienceName)
                .Select(r => new TreeModel
                {
                    Id = r.Id,
                    CommonName = r.CommonName,
                    ScienceName = r.ScienceName,
                    Family = r.Family,
                    TreeDetails = treeDetails.ContainsKey(r.Id) ? treeDetails[r.Id] : Array.Empty<TreeDetailModel>(),
                    IconData = r.IconData
                }).ToList();

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
                Trees = treeModels,
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
            };

            return result;
        }

        private static void UpdatePlotPoint(ICollection<PlotPoint> plotPoints, Tree entityEntry)
        {
            foreach (var point in plotPoints.Where(r => r.Tree?.ScienceName == entityEntry.ScienceName))
            {
                point.TreeId = entityEntry.Id;
                point.Tree = null;
            }
        }
    }
}
