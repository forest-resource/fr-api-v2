using AutoMapper;
using fr.AppServer.Controllers.Abstracts;
using fr.Database.Model.Entities.Trees;
using fr.Service.Interfaces;
using fr.Service.Model.Trees;
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
            IGenericService<Tree, TreeModel> service
            ) : base(mapper, service)
        {
        }

        [HttpGet]
        public Task<IEnumerable<TreeModel>> GetManyAsync([FromQuery] TreeSearchModel searchModel)
            => base.GetManyAsync(searchModel);

        [HttpPost]
        public Task<TreeModel> CreateAsync([FromBody] TreeCreateAndUpdateModel model)
            => base.CreateAsync(model);

        [HttpGet("{id}")]
        public Task<TreeModel> GetOneAsync([FromRoute] Guid id)
            => base.GetOneAsync(id);

        [HttpPut("{id}")]
        public Task<TreeModel> UpdateAsync([FromRoute] Guid id, [FromBody] TreeCreateAndUpdateModel model)
            => base.UpdateAsync(id, model);

        [HttpDelete("{id}")]
        public Task<TreeModel> DeleteAsync([FromRoute] Guid id)
            => base.DeleteAsync(id);
    }
}
