using AutoMapper;
using fr.AppServer.Controllers.Abstracts;
using fr.Database.Model.Entities.Icons;
using fr.Service.FileService;
using fr.Service.IconService;
using fr.Service.Model;
using fr.Service.Model.Icons;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fr.AppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IconsController : ControllerBase<Icon, IconCreateUpdateModel, IconModel, IconSearchModel>
    {
        private readonly IFileService fileService;
        private readonly IIconService iconService;
        public IconsController(IMapper mapper, IIconService service, IFileService fileService)
            : base(mapper, service)
        {
            this.fileService = fileService;
            iconService = service;
        }

        [HttpGet]
        public Task<IEnumerable<IconModel>> GetIconsAsync([FromQuery] IconSearchModel model)
            => base.GetManyAsync(model);

        [HttpGet("get-available-icons")]
        public Task<IEnumerable<IconModel>> GetAvailableIconsAsync()
            => iconService.GetAvailableIconsAsync();

        [HttpGet("{id}")]
        public Task<IconModel> GetIconAsync([FromRoute] Guid id)
            => base.GetOneAsync(id);

        [HttpPost]
        public Task<IconModel> CreateIconAsync([FromBody] IconCreateUpdateModel model)
            => base.CreateAsync(model);

        [HttpPut("{id}")]
        public Task<IconModel> UpdateIconAsync([FromRoute] Guid id, [FromBody] IconCreateUpdateModel model)
            => base.UpdateAsync(id, model);

        [HttpDelete("{id}")]
        public Task DeleteIconAsync([FromRoute] Guid id)
            => base.DeleteAsync(id);

        [HttpPost("upladFile")]
        public Task<bool> UploadFile([FromForm] FileModel model)
            => fileService.CalcSvgFile(model.File);
    }
}
