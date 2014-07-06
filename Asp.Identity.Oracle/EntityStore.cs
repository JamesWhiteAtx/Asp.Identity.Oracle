using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Identity.Oracle
{
    /// <summary>
    ///     EntityFramework based IIdentityEntityStore that allows query/manipulation of a TEntity set
    /// </summary>
    /// <typeparam name="TEntity">Concrete entity type, i.e .User</typeparam>
    internal class EntityStore<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Constructor that takes a Context
        /// </summary>
        /// <param name="context"></param>
        public EntityStore(DbContext context)
        {
            Context = context;
            DbEntitySet = context.Set<TEntity>();
        }

        /// <summary>
        ///     Context for the store
        /// </summary>
        public DbContext Context { get; private set; }

        /// <summary>
        ///     Used to query the entities
        /// </summary>
        public IQueryable<TEntity> EntitySet
        {
            get { return DbEntitySet; }
        }

        /// <summary>
        ///     EntitySet for this store
        /// </summary>
        public DbSet<TEntity> DbEntitySet { get; private set; }


        /// <summary>
        /// wrap sync call with async wait
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public async Task<TResult> WrapWait<TResult>(Func<TResult> function)
        {
            return await Task<TResult>.Run(function);
        }

        /// <summary>
        /// entity framework 5 Async save
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveEF5ChangesAsync()
        {
            return await Task.Run(() => Context.SaveChanges());
        }

        /// <summary>
        ///     FindAsync an entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindByIdAsync(object id)
        {
            //return DbEntitySet.FindAsync(id);
            return WrapWait<TEntity>(() => DbEntitySet.Find(new[] { id }));
        }


        public virtual Task<TEntity> FindByNameAsync(string name)
        {
            return WrapWait<TEntity>(() => DbEntitySet.FirstOrDefault(r => r.Name == name));
        }


        /// <summary>
        ///     Insert an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Create(TEntity entity)
        {
            DbEntitySet.Add(entity);
        }

        /// <summary>
        ///     Mark an entity for deletion
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            DbEntitySet.Remove(entity);
        }

        /// <summary>
        ///     Update an entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            if (entity != null)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}