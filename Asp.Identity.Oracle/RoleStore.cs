using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Asp.Identity.Oracle
{
    public class RoleStore : IQueryableRoleStore<IdentityRole>
    {
        private bool _disposed;

        /// <summary>
        ///     Constructor which takes a db context and wires up the stores with default instances using the context
        /// </summary>
        /// <param name="context"></param>
        public RoleStore(IdentityDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            Context = context;
        }

        /// <summary>
        ///     Context for the store
        /// </summary>
        public IdentityDbContext Context { get; private set; }

        /// <summary>
        ///     If true will call dispose on the DbContext during Dipose
        /// </summary>
        public bool DisposeContext { get; set; }

        public IQueryable<IdentityRole> Roles
        {
            get { return Context.Roles; }
        }

        public Task CreateAsync(IdentityRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            Context.Roles.Add(role);

            return Context.SaveEF5ChangesAsync();
        }

        public Task DeleteAsync(IdentityRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            Context.Roles.Remove(role);
            return Context.SaveEF5ChangesAsync();
        }

        public Task<IdentityRole> FindByIdAsync(string roleId)
        {
            ThrowIfDisposed();
            return Context.WrapWait<IdentityRole>(() => Context.Roles.Find(new[] { roleId }));
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            return Context.WrapWait<IdentityRole>(() => Context.Roles.FirstOrDefault(r => r.Name == roleName));
        }

        public Task UpdateAsync(IdentityRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            Context.Entry(role).State = System.Data.EntityState.Modified;
            return Context.SaveEF5ChangesAsync();

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
        }
    }
}
