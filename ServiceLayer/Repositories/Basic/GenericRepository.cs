using Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using DataLayer;

namespace Service
{
    public class GenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DatabaseContext DbContext;
        protected DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public GenericRepository(DatabaseContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>();
        }

        #region Async Method


        public async virtual Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> where = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includes = "")
        {
            IQueryable<TEntity> query = TableNoTracking;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (includes != "")
            {
                foreach (string include in includes.Split(','))
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }

        public virtual Task<TEntity> GetByIdAsync(params object[] ids)
        {
            return Entities.FindAsync(ids);
        }

        public virtual Task<TEntity> GetByConditionAsync(Expression<Func<TEntity, bool>> where = null, string includes = "", bool isTracked = false)
        {
            var query = isTracked ? Table : TableNoTracking;

            if (where != null) query = query.Where(where);

            if (includes != "")
            {
                foreach (string include in includes.Split(','))
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefaultAsync();
        }

        public virtual Task<TEntity> GetByConditionAsyncTracked(Expression<Func<TEntity, bool>> where = null)
        {
            var query = Table;

            if (where != null) query = query.Where(where);

            return query.FirstOrDefaultAsync();
        }

        public virtual async Task AddAsync(TEntity entity, bool saveNow = true)
        {
            await Entities.AddAsync(entity).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync();
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            await Entities.AddRangeAsync(entities).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity, bool saveNow = true)
        {
            Entities.Update(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Entities.UpdateRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity, bool saveNow = true)
        {
            Entities.Remove(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Entities.RemoveRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync();
        }
        #endregion

        #region Sync Methods

        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> where = null,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includes = "")
        {
            IQueryable<TEntity> query = TableNoTracking;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (includes != "")
            {
                foreach (string include in includes.Split(','))
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }


        public virtual IEnumerable<TEntity> GetListWithTake(Expression<Func<TEntity, bool>> where = null,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includes = "", int take = 0)
        {
            IQueryable<TEntity> query = TableNoTracking;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (includes != "")
            {
                foreach (string include in includes.Split(','))
                {
                    query = query.Include(include);
                }
            }

            if (take != 0)
            {
                query = query.Take(take);
            }

            return query.ToList();
        }

        public virtual TEntity GetById(params object[] ids)
        {
            return Entities.Find(ids);
        }

        public TEntity GetByCondition(Expression<Func<TEntity, bool>> where = null)
        {
            var query = TableNoTracking;

            if (where != null) query = query.Where(where);

            return query.FirstOrDefault();
        }

        public TEntity GetByConditionTracked(Expression<Func<TEntity, bool>> where = null)
        {
            var query = Table;

            if (where != null) query = query.Where(where);

            return query.FirstOrDefault();
        }

        public virtual void Add(TEntity entity, bool saveNow = true)
        {
            Entities.Add(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Entities.AddRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void Update(TEntity entity, bool saveNow = true)
        {
            Entities.Update(entity);
            DbContext.SaveChanges();
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Entities.UpdateRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity, bool saveNow = true)
        {
            Entities.Remove(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }


        public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Entities.RemoveRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }
        #endregion

        #region Attach & Detach
        public virtual void Detach(TEntity entity)
        {
            var entry = DbContext.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }

        public virtual void Attach(TEntity entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);
        }
        #endregion

        #region Explicit Loading
        public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);

            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                await collection.LoadAsync().ConfigureAwait(false);
        }

        public virtual void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);
            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                collection.Load();
        }

        public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                await reference.LoadAsync().ConfigureAwait(false);
        }

        public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                reference.Load();
        }
        #endregion


        #region Save

        public async Task<SweetAlertExtenstion> SaveAsync()
        {
            return await DbContext.SaveChangesAsync() > 0 ? SweetAlertExtenstion.Ok() : SweetAlertExtenstion.Error();
        }

        public SweetAlertExtenstion Save()
        {
            return DbContext.SaveChanges() > 0 ? SweetAlertExtenstion.Ok() : SweetAlertExtenstion.Error();
        }

        #endregion
    }

}
