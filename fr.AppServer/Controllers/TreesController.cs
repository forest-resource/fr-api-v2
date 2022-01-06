using AutoMapper;
using fr.AppServer.Controllers.Abstracts;
using fr.Database.Model.Entities.Trees;
using fr.Service.Model.Trees;
using fr.Service.TreeService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fr.AppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreesController : ControllerBase<Tree, TreeCreateAndUpdateModel, TreeModel, TreeSearchModel>
    {
        public TreesController(
            IMapper mapper,
            ITreeService service
            ) : base(mapper, service)
        {
        }

        [HttpGet]
        public override Task<IEnumerable<TreeModel>> GetManyAsync([FromQuery] TreeSearchModel searchModel)
            => base.GetManyAsync(searchModel);

        [HttpPost]
        public override Task<TreeModel> CreateAsync([FromBody] TreeCreateAndUpdateModel model)
            => base.CreateAsync(model);

        [HttpGet("{id}")]
        public override Task<TreeModel> GetOneAsync([FromRoute] Guid id)
            => base.GetOneAsync(id);

        [HttpPut("{id}")]
        public override Task<TreeModel> UpdateAsync([FromRoute] Guid id, [FromBody] TreeCreateAndUpdateModel model)
            => base.UpdateAsync(id, model);

        [HttpDelete("{id}")]
        public override Task<TreeModel> DeleteAsync([FromRoute] Guid id)
            => base.DeleteAsync(id);
    }
}
