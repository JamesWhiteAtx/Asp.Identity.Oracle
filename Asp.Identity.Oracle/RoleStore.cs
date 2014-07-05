using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

namespace Asp.Identity.Oracle
{

    /// <summary>
    ///     EntityFramework based implementation
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    public class RoleStore<TRole> : RoleStore<TRole, string, IdentityUserRole>, IQueryableRoleStore<TRole>
        where TRole : IdentityRole, new()
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public RoleStore()
            : base(new IdentityDbContext())
        {
            DisposeContext = true;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="context"></param>
        public RoleStore(DbContext context)
            : base(context)
        {
        }
    }

    /// <summary>
    ///     EntityFramework based implementation
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TUserRole"></typeparam>
    public class RoleStore<TRole, TKey, TUserRole> : IQueryableRoleStore<TRole, TKey>
        where TUserRole : IdentityUserRole<TKey>, new()
        where TRole : IdentityRole<TKey, TUserRole>, new()
    {
        private bool _disposed;
        private EntityStore<TRole> _roleStore;

        /// <summary>
        ///     Constructor which takes a db context and wires up the stores with default instances using the context
        /// </summary>
        /// <param name="context"></param>
        public RoleStore(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            Context = context;
            _roleStore = new EntityStore<TRole>(context);
        }

        /// <summary>
        ///     Context for the store
        /// </summary>
        public DbContext Context { get; private set; }

        /// <summary>
        ///     If true will call dispose on the DbContext during Dipose
        /// </summary>
        public bool DisposeContext { get; set; }

        /// <summary>
        ///     Find a role by id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Task<TRole> FindByIdAsync(TKey roleId)
        {
            ThrowIfDisposed();
            return _roleStore.GetByIdAsync(roleId);
        }

        /// <summary>
        ///     Find a role by name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Task<TRole> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            return _roleStore.EntitySet.FirstOrDefaultAsync(u => u.Name.ToUpper() == roleName.ToUpper());
        }

        /// <summary>
        ///     Insert an entity
        /// </summary>
        /// <param name="role"></param>
        public virtual async Task CreateAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            _roleStore.Create(role);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///     Mark an entity for deletion
        /// </summary>
        /// <param name="role"></param>
        public virtual async Task DeleteAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            _roleStore.Delete(role);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///     Update an entity
        /// </summary>
        /// <param name="role"></param>
        public virtual async Task UpdateAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            _roleStore.Update(role);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///     Returns an IQueryable of users
        /// </summary>
        public IQueryable<TRole> Roles
        {
            get { return _roleStore.EntitySet; }
        }

        /// <summary>
        ///     Dispose the store
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        ///     If disposing, calls dispose on the Context.  Always nulls out the Context
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (DisposeContext && disposing && Context != null)
            {
                Context.Dispose();
            }
            _disposed = true;
            Context = null;
            _roleStore = null;
        }
    }

    public class RoleStoreX : IQueryableRoleStore<IdentityUserRole, string>
    {
        private readonly IdentityDbContext db;

        public RoleStoreX(IdentityDbContext db)
        {
            this.db = db;
        }

        //// IQueryableRoleStore<UserRole, TKey>

        public IQueryable<IdentityUserRole> Roles
        {
            get { return this.db.IdentityUserRoles; }
        }

        //// IRoleStore<UserRole, TKey>

        public virtual Task CreateAsync(IdentityUserRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this.db.IdentityUserRoles.Add(role);
            return this.db.SaveEF5ChangesAsync();
        }

        public Task DeleteAsync(IdentityUserRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this.db.IdentityUserRoles.Remove(role);
            return this.db.SaveEF5ChangesAsync();
        }

        public Task<IdentityUserRole> FindByIdAsync(string roleId)
        {
            //return this.db.IdentityUserRoles.FindAsync(new[] { roleId });
            return db.WrapWait<IdentityUserRole>(() => this.db.IdentityUserRoles.Find(new[] { roleId }));
        }

        public Task<IdentityUserRole> FindByNameAsync(string roleName)
        {
            //return this.db.IdentityUserRoles.FirstOrDefaultAsync(r => r.Name == roleName);
            return db.WrapWait<IdentityUserRole>(() => this.db.IdentityUserRoles.FirstOrDefault(r => r.Name == roleName));
        }

        public Task UpdateAsync(IdentityUserRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this.db.Entry(role).State = System.Data.EntityState.Modified;
            return this.db.SaveEF5ChangesAsync();
        }

        //// IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.db != null)
            {
                this.db.Dispose();
            }
        }
    }
}
