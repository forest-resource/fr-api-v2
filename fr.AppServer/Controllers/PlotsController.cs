using AutoMapper;
using fr.AppServer.Controllers.Abstracts;
using fr.AppServer.Infrastructor.Attributes;
using fr.Database.Model.Entities.Plots;
using fr.Service.FileService;
using fr.Service.Model.Plots;
using fr.Service.PlotService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fr.AppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlotsController : ControllerBase<Plot, PlotCreateUpdateModel, PlotModel, PlotSearchModel>
    {
        public PlotsController(
            IMapper mapper,
            IPlotService service
            ) : base(mapper, service)
        { }

        [HttpGet]
        public override Task<IEnumerable<PlotModel>> GetManyAsync([FromQuery] PlotSearchModel model)
            => base.GetManyAsync(model);

        [HttpPost]
        public override Task<PlotModel> CreateAsync([FromBody] PlotCreateUpdateModel model)
            => base.CreateAsync(model);

        [HttpGet("{id}")]
        public override Task<PlotModel> GetOneAsync([FromRoute] Guid id)
            => base.GetOneAsync(id);

        [HttpPut("{id}")]
        public override Task<PlotModel> UpdateAsync([FromRoute] Guid id, [FromBody] PlotCreateUpdateModel model)
            => base.UpdateAsync(id, model);

        [HttpDelete("{id}")]
        public override  Task<PlotModel> DeleteAsync([FromQuery] Guid id)
            => base.DeleteAsync(id);

        [HttpPost("upload-plot-points-file")]
        [FileRequiredFilter]
        public Task<IEnumerable<PlotPointFromFileModel>> UploadPlotPointsAsync([FromServices] IFileService fileService)
            => fileService.ReadPlotPointsAsync();
    }
}
