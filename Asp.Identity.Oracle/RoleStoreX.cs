using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Identity.Oracle
{
    //public class RoleStoreX : IQueryableRoleStore<IdentityUserRole, string>
    //{
    //    private readonly IdentityDbContext db;

    //    public RoleStoreX(IdentityDbContext db)
    //    {
    //        this.db = db;
    //    }

    //    //// IQueryableRoleStore<UserRole, TKey>

    //    public IQueryable<IdentityUserRole> Roles
    //    {
    //        get { return this.db.IdentityUserRoles; }
    //    }

    //    //// IRoleStore<UserRole, TKey>

    //    public virtual Task CreateAsync(IdentityUserRole role)
    //    {
    //        if (role == null)
    //        {
    //            throw new ArgumentNullException("role");
    //        }

    //        this.db.IdentityUserRoles.Add(role);
    //        return this.db.SaveEF5ChangesAsync();
    //    }

    //    public Task DeleteAsync(IdentityUserRole role)
    //    {
    //        if (role == null)
    //        {
    //            throw new ArgumentNullException("role");
    //        }

    //        this.db.IdentityUserRoles.Remove(role);
    //        return this.db.SaveEF5ChangesAsync();
    //    }

    //    public Task<IdentityUserRole> FindByIdAsync(string roleId)
    //    {
    //        //return this.db.IdentityUserRoles.FindAsync(new[] { roleId });
    //        return db.WrapWait<IdentityUserRole>(() => this.db.IdentityUserRoles.Find(new[] { roleId }));
    //    }

    //    public Task<IdentityUserRole> FindByNameAsync(string roleName)
    //    {
    //        //return this.db.IdentityUserRoles.FirstOrDefaultAsync(r => r.Name == roleName);
    //        return db.WrapWait<IdentityUserRole>(() => this.db.IdentityUserRoles.FirstOrDefault(r => r.Name == roleName));
    //    }

    //    public Task UpdateAsync(IdentityUserRole role)
    //    {
    //        if (role == null)
    //        {
    //            throw new ArgumentNullException("role");
    //        }

    //        this.db.Entry(role).State = System.Data.EntityState.Modified;
    //        return this.db.SaveEF5ChangesAsync();
    //    }

    //    //// IDisposable

    //    public void Dispose()
    //    {
    //        this.Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (disposing && this.db != null)
    //        {
    //            this.db.Dispose();
    //        }
    //    }
    //}
}
