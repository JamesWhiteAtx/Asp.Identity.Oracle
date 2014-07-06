// Copyright (c) KriaSoft, LLC.  All rights reserved.  See LICENSE.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Asp.Identity.Oracle;

namespace Asp.Identity.Oracle
{
    public class UserStoreX<TUser, TKey, TRole, TUserLogin, TUserClaim> : 
        IUserStore<TUser, TKey>,
        IQueryableUserStore<TUser, TKey>, 
        IUserPasswordStore<TUser, TKey>, 
        IUserLoginStore<TUser, TKey>,
        IUserClaimStore<TUser, TKey>, 
        IUserRoleStore<TUser, TKey>, 
        IUserSecurityStampStore<TUser, TKey>,
        IUserEmailStore<TUser, TKey>, 
        IUserPhoneNumberStore<TUser, TKey>,
        IUserTwoFactorStore<TUser, TKey>,
        IUserLockoutStore<TUser, TKey>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser
        where TRole : IdentityRole
        where TUserLogin : IdentityUserLogin, new()
        where TUserClaim : IdentityUserClaim, new()

    {
        private bool _disposed;

        /// <summary>
        ///     Constructor which takes a db context and wires up the stores with default instances using the context
        /// </summary>
        /// <param name="context"></param>
        public UserStoreX(IdentityDbContext context)
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

        //IDisposable
        #region IDisposable

        /// <summary>
        ///     If true will call dispose on the DbContext during Dipose
        /// </summary>
        public bool DisposeContext { get; set; }

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

        #endregion IDisposable

        // IQueryableUserStore
        #region IQueryableUserStore
        
        public IQueryable<IdentityUser> Users
        {
            get { throw new NotImplementedException(); }
        }

        #endregion IQueryableUserStore

        //IUserStore
        #region IUserStore

        public Task CreateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            return Context.WrapWait<IdentityUser>(() =>
                Context.Users
                    .Include(u => u.Logins).Include(u => u.Roles).Include(u => u.Claims)
                    .FirstOrDefault(u => u.Id.Equals(userId))
                );
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        #endregion IUserStore

        //IUserPasswordStore
        #region IUserPasswordStore

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        #endregion IUserPasswordStore

        // IUserLoginStore
        #region IUserLoginStore

        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        #endregion IUserLoginStore

        // IUserClaimStore
        #region IUserClaimStore

        public Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        #endregion IUserClaimStore

        // IUserRoleStore
        #region IUserRoleStore

        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion IUserRoleStore

        // IUserSecurityStampStore
        #region IUserSecurityStampStore

        public Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            throw new NotImplementedException();
        }

        #endregion IUserSecurityStampStore

        // IUserEmailStore
        #region IUserEmailStore

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(IdentityUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        #endregion IUserEmailStore

        // IUserPhoneNumberStore
        #region IUserPhoneNumberStore

        public Task<string> GetPhoneNumberAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberAsync(IdentityUser user, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberConfirmedAsync(IdentityUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        #endregion IUserPhoneNumberStore

        //IUserTwoFactorStore
        #region IUserTwoFactorStore

        public Task<bool> GetTwoFactorEnabledAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetTwoFactorEnabledAsync(IdentityUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        #endregion IUserTwoFactorStore

        // IUserLockoutStore
        #region IUserLockoutStore

        public Task<int> GetAccessFailedCountAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetLockoutEnabledAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(IdentityUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        #endregion IUserLockoutStore

    }

    public partial class UserStore :
        IQueryableUserStore<IdentityUser, string>, IUserPasswordStore<IdentityUser, string>, IUserLoginStore<IdentityUser, string>,
        IUserClaimStore<IdentityUser, string>, IUserRoleStore<IdentityUser, string>, IUserSecurityStampStore<IdentityUser, string>,
        IUserEmailStore<IdentityUser, string>, IUserPhoneNumberStore<IdentityUser, string>, IUserTwoFactorStore<IdentityUser, string>,
        IUserLockoutStore<IdentityUser, string>
    {
        private readonly IdentityDbContext db;

        public UserStore(IdentityDbContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            this.db = db;
        }

        //// IQueryableUserStore<IdentityUser, int>

        public IQueryable<IdentityUser> Users
        {
            get { return this.db.IdentityUsers; }
        }

        //// IUserStore<IdentityUser, Key>

        public Task CreateAsync(IdentityUser user)
        {
            this.db.IdentityUsers.Add(user);
            return this.db.SaveEF5ChangesAsync(); //this.db.SaveEF5ChangesAsync();
        }

        public Task DeleteAsync(IdentityUser user)
        {
            this.db.IdentityUsers.Remove(user);
            return this.db.SaveEF5ChangesAsync();
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            //return this.db.IdentityUsers
            //    .Include(u => u.Logins).Include(u => u.Roles).Include(u => u.Claims)
            //    .FirstOrDefaultAsync(u => u.Id.Equals(userId));
            return db.WrapWait<IdentityUser>(() => 
                this.db.IdentityUsers
                    .Include(u => u.Logins).Include(u => u.Roles).Include(u => u.Claims)
                    .FirstOrDefault(u => u.Id.Equals(userId))
                );
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            //return this.db.IdentityUsers
            //    .Include(u => u.Logins).Include(u => u.Roles).Include(u => u.Claims)
            //    .FirstOrDefaultAsync(u => u.UserName == userName);
            return db.WrapWait<IdentityUser>(() =>
                this.db.IdentityUsers
                    .Include(u => u.Logins).Include(u => u.Roles).Include(u => u.Claims)
                    .FirstOrDefault(u => u.UserName == userName)
                );
        }

        public Task UpdateAsync(IdentityUser user)
        {
            this.db.Entry<IdentityUser>(user).State = System.Data.EntityState.Modified;
            return this.db.SaveEF5ChangesAsync();
        } 

        //// IUserPasswordStore<IdentityUser, Key>

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        } 

        //// IUserLoginStore<IdentityUser, Key>

        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var userLogin = Activator.CreateInstance<IdentityUserLogin>();
            userLogin.UserId = user.Id;
            userLogin.LoginProvider = login.ProviderKey;
            userLogin.ProviderKey = login.ProviderKey;
            user.Logins.Add(userLogin);
            return Task.FromResult(0);
        }

        public async Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var provider = login.LoginProvider;
            var key = login.ProviderKey;

            //var userLogin = await this.db.IdentityUserLogins.FirstOrDefaultAsync(l => l.LoginProvider == provider && l.ProviderKey == key);
            var userLogin = await db.WrapWait<IdentityUserLogin>(() => this.db.IdentityUserLogins.FirstOrDefault(l => l.LoginProvider == provider && l.ProviderKey == key));

            if (userLogin == null)
            {
                return default(IdentityUser);
            }

            //return await this.db.IdentityUsers
            //    .Include(u => u.Logins).Include(u => u.Roles).Include(u => u.Claims)
            //    .FirstOrDefaultAsync(u => u.Id.Equals(userLogin.UserId));
            return await db.WrapWait<IdentityUser>(() => 
                this.db.IdentityUsers
                    .Include(u => u.Logins).Include(u => u.Roles).Include(u => u.Claims)
                    .FirstOrDefault(u => u.Id.Equals(userLogin.UserId))
                );
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<IList<UserLoginInfo>>(user.Logins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList());
        }

        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var provider = login.LoginProvider;
            var key = login.ProviderKey;

            var item = user.Logins.SingleOrDefault(l => l.LoginProvider == provider && l.ProviderKey == key);

            if (item != null)
            {
                user.Logins.Remove(item);
            }

            return Task.FromResult(0);
        } 

        //// IUserClaimStore<IdentityUser, int>

        public Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            var item = Activator.CreateInstance<IdentityUserClaim>();
            item.UserId = user.Id;
            item.ClaimType = claim.Type;
            item.ClaimValue = claim.Value;
            user.Claims.Add(item);
            return Task.FromResult(0);
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<IList<Claim>>(user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList());
        }

        public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            foreach (var item in user.Claims.Where(uc => uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type).ToList())
            {
                user.Claims.Remove(item);
            }

            foreach (var item in this.db.IdentityUserClaims.Where(uc => uc.UserId.Equals(user.Id) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type).ToList())
            {
                this.db.IdentityUserClaims.Remove(item);
            }

            return Task.FromResult(0);
        } 

        //// IUserRoleStore<IdentityUser, int>

        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Value Cannot Be Null Or Empty", "roleName");
            }

            var userRole = this.db.IdentityUserRoles.SingleOrDefault(r => r.Name == roleName);

            if (userRole == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Role {0} does not exist.", new object[] { roleName }));
            }

            user.Roles.Add(userRole);
            return Task.FromResult(0);
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<IList<string>>(user.Roles.Join(this.db.IdentityUserRoles, ur => ur.Id, r => r.Id, (ur, r) => r.Name).ToList());
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Value Cannot Be Null Or Empty", "roleName");
            }

            return
                Task.FromResult<bool>(
                    this.db.IdentityUserRoles.Any(r => r.Name == roleName && r.Users.Any(u => u.Id.Equals(user.Id))));
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Value Cannot Be Null Or Empty", "roleName");
            }

            var userRole = user.Roles.SingleOrDefault(r => r.Name == roleName);

            if (userRole != null)
            {
                user.Roles.Remove(userRole);
            }

            return Task.FromResult(0);
        } 

        //// IUserSecurityStampStore<IdentityUser, int>

        public Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        } 

        //// IUserEmailStore<IdentityUser, int>

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            //return this.db.IdentityUsers
            //    .Include(u => u.Logins).Include(u => u.Roles).Include(u => u.Claims)
            //    .FirstOrDefaultAsync(u => u.Email == email);

            return db.WrapWait<IdentityUser>(() => 
                this.db.IdentityUsers
                    .Include(u => u.Logins).Include(u => u.Roles).Include(u => u.Claims)
                    .FirstOrDefault(u => u.Email == email)
                );
        }

        public Task<string> GetEmailAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailAsync(IdentityUser user, string email)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.Email = email;
            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        //// IUserPhoneNumberStore<IdentityUser, int>

        public Task<string> GetPhoneNumberAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberAsync(IdentityUser user, string phoneNumber)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberConfirmedAsync(IdentityUser user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        //// IUserTwoFactorStore<IdentityUser, int>

        public Task<bool> GetTwoFactorEnabledAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(IdentityUser user, bool enabled)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        //// IUserLockoutStore<IdentityUser, int>

        public Task<int> GetAccessFailedCountAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(
                user.LockoutEndDateUtc.HasValue ?
                    new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc)) :
                    new DateTimeOffset());
        }

        public Task<int> IncrementAccessFailedCountAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task SetLockoutEnabledAsync(IdentityUser user, bool enabled)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset lockoutEnd)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? null : new DateTime?(lockoutEnd.UtcDateTime);
            return Task.FromResult(0);
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
