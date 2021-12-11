using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using fr.Database.Model.Interfaces;
using fr.Service.Interfaces;
using fr.Core.Exceptions;
using fr.Core.Expressions;
using fr.Database.EntityFramework;
using fr.Database.Model.Abstracts;

namespace fr.Service.Generic
{
    public class GenericService<TEntity, TModel> : IGenericService<TEntity, TModel>
        where TEntity : class, IKeyModel
    {
        protected readonly IMapper _mapper;

        protected readonly DbContext DbContext;
        protected readonly DbSet<TEntity> Entities;

        public GenericService(IDesignTimeDbContextFactory dbContextFactory, IMapper mapper)
        {
            DbContext = dbContextFactory.DbContext;
            Entities = DbContext.Set<TEntity>();
            _mapper = mapper;
        }

        public virtual async Task<TModel> CreateAsync(TEntity model)
        {
            var result = await Entities.AddAsync(model);
            await DbContext.SaveChangesAsync();
            return _mapper.Map<TEntity, TModel>(result.Entity);
        }

        public virtual async Task<TModel> UpdateAsync(Guid id, TEntity model)
        {
            var entity = await Entities.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Cannot Find Entity with Id {id}");
            }

            entity = GetEntityFromRequest(entity, model);

            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();

            return _mapper.Map<TModel>(model);
        }

        public virtual async Task<TModel> DeleteAsync(Guid id)
        {
            var entity = await Entities.FindAsync(id);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Cannot Find Entity with Id {id}");
            }

            var result = DbContext.Remove(entity);

            await DbContext.SaveChangesAsync();
            return _mapper.Map<TModel>(result.Entity);
        }

        public virtual IEnumerable<TModel> DeleteMany(ExpressionRule model)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TModel> GetAsync(Guid key)
        {
            TEntity entity = await Entities.FindAsync(key);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Cannot find {typeof(TEntity).Name} with key: {key}");
            }

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<IEnumerable<TModel>> GetManyAsync(ExpressionRule model)
            => await (await GetMany<TModel>(model)).ToListAsync();

        protected async Task<IQueryable<T>> GetMany<T>(ExpressionRule model)
        {
            var query = model.All(r => r.Value == null)
                ? Entities
                : Entities.Where(model.Build<TEntity>());

            await query.LoadAsync();

            return query.ProjectTo<T>(_mapper.ConfigurationProvider);
        }

        protected TDest GetEntityFromRequest<TSource, TDest>(TDest entity, TSource model)
        {
            var currentObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(entity));
            var newObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(model));

            var keys = currentObj.Keys.GroupJoin(
                FullEntityModel.AudittingProps,
                r => r,
                r => r,
                (left, right) => !right.Any() ? left : string.Empty
            ).Where(r => !string.IsNullOrEmpty(r));

            foreach (var key in keys)
            {
                currentObj[key] = newObj[key];
            }

            return JsonConvert.DeserializeObject<TDest>(JsonConvert.SerializeObject(currentObj));
        }
    }
}
