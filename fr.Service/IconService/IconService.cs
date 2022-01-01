using AutoMapper;
using AutoMapper.QueryableExtensions;
using fr.Database.EntityFramework;
using fr.Database.Model.Entities.Icons;
using fr.Database.Model.Entities.Trees;
using fr.Service.Generic;
using fr.Service.Model.Icons;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fr.Service.IconService
{
    public class IconService : GenericService<Icon, IconModel>, IIconService
    {
        public IconService(IDesignTimeDbContextFactory dbContextFactory, IMapper mapper)
            : base(dbContextFactory, mapper)
        {
        }

        async Task<IEnumerable<IconModel>> IIconService.GetAvailableIconsAsync()
        {
            var iconQuery = Entities.AsNoTracking();

            var result = iconQuery
                .Where(r => r.Tree == null)
                .ProjectTo<IconModel>(_mapper.ConfigurationProvider);

            return await result.ToListAsync();
        }
    }
}
