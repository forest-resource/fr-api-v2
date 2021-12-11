using fr.Core.Expressions;
using fr.Database.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fr.Service.Interfaces
{
    public interface IGenericService<TEntity, TResponseModel> where TEntity : class, IKeyModel
    {
        Task<TResponseModel> CreateAsync(TEntity model);

        Task<TResponseModel> UpdateAsync(Guid id, TEntity entity);

        Task<TResponseModel> DeleteAsync(Guid id);

        IEnumerable<TResponseModel> DeleteMany(ExpressionRule model);

        Task<TResponseModel> GetAsync(Guid model);

        Task<IEnumerable<TResponseModel>> GetManyAsync(ExpressionRule model);

    }
}