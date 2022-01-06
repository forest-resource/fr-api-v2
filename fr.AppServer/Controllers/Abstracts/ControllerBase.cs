using AutoMapper;
using fr.Core.Expressions;
using fr.Database.Model.Interfaces;
using fr.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fr.AppServer.Controllers.Abstracts
{
    public abstract class ControllerBase<TEntity, TRequestModel, TResponseModel, TSearchModel> : ControllerBase
        where TEntity : class, IKeyModel
        where TRequestModel : class
        where TResponseModel : class
        where TSearchModel : class, new()
    {
        protected virtual IMapper Mapper { get; }
        protected virtual IGenericService<TEntity, TResponseModel> Service { get; }

        protected ControllerBase(IMapper mapper, IGenericService<TEntity, TResponseModel> service)
        {
            Mapper = mapper;
            Service = service;
        }

        public virtual Task<TResponseModel> CreateAsync(TRequestModel model)
            => Service.CreateAsync(Mapper.Map<TEntity>(model));

        public virtual Task<TResponseModel> DeleteAsync(Guid id)
            => Service.DeleteAsync(id);

        public virtual Task<TResponseModel> UpdateAsync(Guid id, TRequestModel model)
            => Service.UpdateAsync(id, Mapper.Map<TEntity>(model));

        public virtual Task<TResponseModel> GetOneAsync(Guid id)
            => Service.GetAsync(id);

        public virtual Task<IEnumerable<TResponseModel>> GetManyAsync(TSearchModel model)
            => Service.GetManyAsync(MapFromModel(model ?? default));

        protected ExpressionRule MapFromModel(TSearchModel model)
            => Mapper.Map<object, ExpressionRule>(model);
    }
}
