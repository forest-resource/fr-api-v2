using AutoMapper;
using fr.Core.Exceptions;
using fr.Database.EntityFramework;
using fr.Database.Model.Entities.Trees;
using fr.Service.Generic;
using fr.Service.Model.Trees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace fr.Service.TreeService
{
    public class TreeService : GenericService<Tree, TreeModel> , ITreeService
    {
        public TreeService(IDesignTimeDbContextFactory dbContextFactory, IMapper mapper)
            : base(dbContextFactory, mapper)
        { }

        public override async Task<TreeModel> UpdateAsync(Guid id, Tree model)
        {
            var treeEntity = await Entities.FindAsync(id);
            if (treeEntity == null)
            {
                throw new EntityNotFoundException($"Not found Tree of id: {id}");
            }

            var treeDetailKeysQuery = await DbContext.Set<TreeDetail>().AsNoTracking()
                .Where(r => r.TreeId == id)
                .Select(r => r.Key)
                .ToListAsync();

            foreach (var key in treeDetailKeysQuery.Union(model.TreeDetails.Select(r => r.Key)))
            {
                if (!DbContext.Set<TreeDetail>().AsNoTracking().Any(r => r.TreeId == id && r.Key == key))
                {
                    DbContext.Add(new TreeDetail
                    {
                        Key = key,
                        Value = model.TreeDetails.FirstOrDefault(r => r.Key == key).Value
                    });
                    continue;
                }

                var treeDetail = DbContext.Set<TreeDetail>().First(r => r.Key == key);

                if (!model.TreeDetails.Any(r => r.TreeId == id && r.Key == key))
                {
                    DbContext.Remove(treeDetail);
                    continue;
                }

                treeDetail.Value = model.TreeDetails.FirstOrDefault(r => r.Key == key)?.Value;
            }

            treeEntity.CommonName = model.CommonName;
            treeEntity.ScienceName = model.ScienceName;
            treeEntity.Family = model.Family;
            treeEntity.IconId = model.IconId;

            await DbContext.SaveChangesAsync();

            return new TreeModel
            {
                Id = treeEntity.Id,
                CommonName = treeEntity.CommonName,
                Family = treeEntity.Family,
                ScienceName = treeEntity.ScienceName,
                IconId = treeEntity.IconId,
                IconData = treeEntity.Icon.IconData
            };
        }
    }
}
