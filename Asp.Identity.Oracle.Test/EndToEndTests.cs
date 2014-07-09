using System;
using System.Linq;
using System.Data;
using System.Data.Entity;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace Asp.Identity.Oracle.Test
{
    [TestClass]
    public class EndToEndTests
    {
        IdentityDbContext context;
        UserStore userStore;
        RoleStore roleStore;

        [TestInitialize]
        public void Setup()
        {
            context = new IdentityDbContext();
            userStore = new UserStore(context);
            roleStore = new RoleStore(context);
        }

        public void Cleaup()
        {
            roleStore.Dispose();
            userStore.Dispose();
            context.Dispose();
        }

        private string testUserName = "test_user";
        private string testEmail = "bobs@your.uncle";

        private void CreateUser(IdentityUser user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        private IdentityUser FindTestUserByName()
        {
            return context.Users.Include(u => u.Roles)
                .Include(u => u.Claims)
                .Include(u => u.Logins)
                .FirstOrDefault(u => u.UserName == testUserName);
        }

        private IdentityUser MakeTestUser()
        {
            var user = new IdentityUser(testUserName);
            user.Email = testEmail;
            return user;
        }

        private IdentityUser FindTestUser()
        {
            var found = FindTestUserByName();
            if (found == null)
            {
                CreateUser(MakeTestUser());
                found = FindTestUserByName();
            }
            return found;
        }

        // test user validation

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Users with duplicate names were allowed")]
        public void Dont_add_duplicate_named_users()
        {
            var testUser = FindTestUser();
            var newUser = new IdentityUser(testUser.UserName);
            CreateUser(newUser);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Duplicate Users without names were allowed")]
        public void Dont_add_duplicate_no_named_users()
        {
            var newUser = new IdentityUser();
            CreateUser(newUser);
            var nextUser = new IdentityUser();
            CreateUser(nextUser);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Users with duplicate emails were allowed")]
        public void Dont_add_duplicate_email_users()
        {
            var testUser = FindTestUser();
            testUser.Email = testEmail;
            context.SaveChanges();

            var newUser = new IdentityUser();
            newUser.Email = testEmail;

            context.RequireUniqueEmail = true;
            CreateUser(newUser);
        }

        // test role validation

        private string testRoleName = "test_role";

        private void CreateRole(IdentityRole role)
        {
            context.Roles.Add(role);
            context.SaveChanges();
        }

        private IdentityRole FindTestRoleByName()
        {
            return context.Roles.FirstOrDefault(r => r.Name == testRoleName);
        }

        private IdentityRole FindTestRole()
        {
            var found = FindTestRoleByName();
            if (found == null)
            {
                var role = new IdentityRole(testRoleName);
                CreateRole(role);
                found = FindTestRoleByName();
            }
            return found;
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Roles with duplicate names were allowed")]
        public void Dont_add_duplicate_named_roles()
        {
            var testRole = FindTestRole();
            var newRole = new IdentityRole(testRole.Name);
            CreateRole(newRole);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Duplicate Roles without names were allowed")]
        public void Dont_add_duplicate_no_named_roles()
        {
            var newRole = new IdentityRole();
            CreateRole(newRole);
            
            var nextRole = new IdentityRole();
            CreateRole(nextRole);
        }


        private async Task<IdentityUser> FindTestUserAsync()
        {
            var found = await userStore.FindByNameAsync(testUserName);
            if (found == null)
            {
                var user = MakeTestUser();
                await userStore.CreateAsync(user);
                found = await userStore.FindByNameAsync(testUserName);
            }
            return found;
        }

        // IQueryableUserStore

        [TestMethod]
        public void UserStore_Users_is_Queryable_of_IdentityUser()
        {
            var users = userStore.Users;
            Assert.IsInstanceOfType(users, typeof(IQueryable<IdentityUser>));
        }

        // Tests for IUserStore

        [TestMethod]
        public async Task UserStore_Create_adds_1_user()
        {
            var testUser = FindTestUserByName();
            if (testUser != null) {
                context.Users.Remove(testUser);
                context.SaveChanges();
            }

            int orig = userStore.Users.Count();

            var user = MakeTestUser();
            var newId = user.Id;
           
            await userStore.CreateAsync(user);

            int added = userStore.Users.Count();
            
            Assert.AreEqual(added, orig+1, "One User was added");

            var found = await userStore.FindByIdAsync(newId);

            Assert.IsInstanceOfType(found, typeof(IdentityUser), "Found new IdentityUser for new Id");

            await userStore.DeleteAsync(found);

            int deleted = userStore.Users.Count();

            Assert.AreEqual(deleted, added - 1, "One User was removed");

            found = await userStore.FindByIdAsync(newId);

            Assert.IsNull(found, "New User should not be found by Id");
        }

        [TestMethod]
        public async Task UserStore_find_by_name()
        {
            var found = await FindTestUserAsync();
            Assert.IsInstanceOfType(found, typeof(IdentityUser), "Found new IdentityUser for new Id");

            Assert.AreEqual(testUserName, found.UserName, "Found user has test user name");

            string name = found.UserName;

            string email;
            if (found.Email == testEmail) {
                email = "bobs@notyour.uncle";
            } else {
                email = testEmail;
            }
            found.Email = email;

            await userStore.UpdateAsync(found);
            found = await userStore.FindByNameAsync(name);
            Assert.AreEqual(found.Email, email, "Email is the updated value");
        }

        // IUserPasswordStore

        [TestMethod]
        public async Task UserStore_password()
        {
            var testPw = "CornPone";
            var user = await FindTestUserAsync();

            user.PasswordHash = "possum";
            Assert.AreNotEqual(user.PasswordHash, testPw, "Store GetPasswordHashAsync get password hash");
            
            await userStore.SetPasswordHashAsync(user, testPw);

            Assert.AreEqual(user.PasswordHash, testPw, "Store GetPasswordHashAsync get password hash");
            
            var pw = await userStore.GetPasswordHashAsync(user);

            Assert.AreEqual(pw, testPw, "Store GetPasswordHashAsync get password hash");

            var hasPw = await userStore.HasPasswordAsync(user);
            Assert.IsTrue(hasPw, "store HasPasswordAsync true if has password");
        }

        // IUserLoginStore

        [TestMethod]
        public async Task UserStore_login_asyncs()
        {
            var testUser = await FindTestUserAsync();
            Assert.IsInstanceOfType(testUser.Logins, typeof(ICollection<IdentityUserLogin>), "User Logins is collection of IdentityUserLogin");

            string testLoginProvider = "testLoginProvider";
            string testProviderKey = "testProviderKey";

            var testLogin = new UserLoginInfo(testLoginProvider, testProviderKey);

            var loginUser = await userStore.FindAsync(testLogin);
            if (loginUser == null)
            {
                await userStore.AddLoginAsync(testUser, testLogin);
                await userStore.UpdateAsync(testUser);

                loginUser = await userStore.FindAsync(testLogin);
            }
            Assert.IsNotNull(loginUser, "Found user for login");

            var logins = await userStore.GetLoginsAsync(loginUser);
            Assert.IsInstanceOfType(logins, typeof(IList<UserLoginInfo>), "GetLoginsAsync is IList of UserLoginInfo");

            var addCount = logins.Count();
            Assert.IsTrue(addCount > 0, "Found user has some logins");

            var login = logins.FirstOrDefault(l => l.LoginProvider == testLoginProvider && l.ProviderKey == testProviderKey);
            Assert.IsInstanceOfType(login, typeof(UserLoginInfo), "Found user has searched for login");

            await userStore.RemoveLoginAsync(loginUser, login);
            await userStore.UpdateAsync(testUser);
            logins = await userStore.GetLoginsAsync(loginUser);
            var delCount = logins.Count();

            Assert.AreEqual(delCount, addCount - 1, "One less login after removing login");

            loginUser = await userStore.FindAsync(testLogin);
            Assert.IsNull(loginUser, "No user found for login after removal");
        }

        //IUserClaimStore

        [TestMethod]
        public async Task User_claims_asyncs()
        {
            var testUser = await FindTestUserAsync();
            Assert.IsInstanceOfType(testUser.Claims, typeof(ICollection<IdentityUserClaim>), "IdentityUser Claims is collection of IdentityUserClaim");

            //Task<IList<Claim>> GetClaimsAsync(TUser user)
            var claims = await userStore.GetClaimsAsync(testUser);
            Assert.IsInstanceOfType(claims, typeof(IList<Claim>), "store GetClaimsAsync Claims is collection of Claim");

            string testType = "testClaimType";
            string testValue = "testClaimValue";
            Claim testClaim = new Claim(testType, testValue);

            await userStore.AddClaimAsync(testUser, testClaim);
            await userStore.UpdateAsync(testUser);
            claims = await userStore.GetClaimsAsync(testUser);
            var addCount = claims.Count();
            Assert.IsTrue(addCount > 0, "User has some claims after add claims");

            await userStore.RemoveClaimAsync(testUser, testClaim);
            await userStore.UpdateAsync(testUser);
            claims = await userStore.GetClaimsAsync(testUser);
            var delCount = claims.Count();

            Assert.AreEqual(delCount, addCount - 1, "One less claim after removing claim");
        }

        // IUserSecurityStampStore

        [TestMethod]
        public async Task User_security_stamp_asyncs()
        {
            var testUser = await FindTestUserAsync();

            testUser.SecurityStamp = "no the test stamp";
            string testStamp = "testStamp";
            var stamp = await userStore.GetSecurityStampAsync(testUser);
            Assert.AreNotEqual(stamp, testStamp, "");

            await userStore.SetSecurityStampAsync(testUser, testStamp);
            stamp = await userStore.GetSecurityStampAsync(testUser);
            Assert.AreEqual(stamp, testStamp, "Store set stamp changes stamp");
        }

        // IUserEmailStore

        [TestMethod]
        public async Task User_email_store_asyncs()        
        {
            var testEmail = "bobs@your.uncle";
            
            var testUser = await FindTestUserAsync();
            var testConf = !testUser.EmailConfirmed;
            
            await userStore.SetEmailAsync(testUser, testEmail);
            await userStore.SetEmailConfirmedAsync(testUser, testConf);
            
            await userStore.UpdateAsync(testUser);
            var foundUser = await userStore.FindByEmailAsync(testEmail);
            
            Assert.IsInstanceOfType(foundUser, typeof(IdentityUser), "Found IdentityUser by Email");

            var curEmail = await userStore.GetEmailAsync(testUser);
            var curConf = await userStore.GetEmailConfirmedAsync(testUser);

            Assert.AreEqual(testEmail, curEmail, "GetEmailAsync email of found user is the searched for email");
            Assert.AreEqual(testConf, curConf, "email confirmed was changed");

        }

        // IUserPhoneNumberStore

        [TestMethod]
        public async Task User_phone_store_asyncs()
        {
            var testUser = await FindTestUserAsync();

            string testPhone;
            if (testUser.PhoneNumber == "8675309")
            {
                testPhone = "9035768";
            }
            else
            {
                testPhone = "8675309";
            }
            var testConf = !testUser.PhoneNumberConfirmed;

            await userStore.SetPhoneNumberAsync(testUser, testPhone);
            await userStore.SetPhoneNumberConfirmedAsync(testUser, testConf);
            await userStore.UpdateAsync(testUser);

            testUser = await FindTestUserAsync();
            var curPhone = await userStore.GetPhoneNumberAsync(testUser);
            var curConf = await userStore.GetPhoneNumberConfirmedAsync(testUser);

            Assert.AreEqual(testPhone, curPhone, "GetPhoneNumberAsync phone matches that set");
            Assert.AreEqual(testConf, curConf, "phone confirmed was changed");
            
        }

        // IUserTwoFactorStore

        [TestMethod]
        public async Task User_twofactor_store_asyncs()
        {
            var testUser = await FindTestUserAsync();
            var test2fact = !await userStore.GetTwoFactorEnabledAsync(testUser);

            await userStore.SetTwoFactorEnabledAsync(testUser, test2fact);
            await userStore.UpdateAsync(testUser);

            testUser = await FindTestUserAsync();
            var cur2Fact = await userStore.GetTwoFactorEnabledAsync(testUser);
            Assert.AreEqual(test2fact, cur2Fact, "two factor enabled was changed");
        }

        // IUserLockoutStore

        [TestMethod]
        public async Task User_lockout_asyncs()
        {
            var testUser = await FindTestUserAsync();
            var testCount = await userStore.GetAccessFailedCountAsync(testUser);
            var testLock = !await userStore.GetLockoutEnabledAsync(testUser);
            var end = await userStore.GetLockoutEndDateAsync(testUser);
            var testEnd = end.AddDays(1);

            await userStore.IncrementAccessFailedCountAsync(testUser);
            await userStore.SetLockoutEnabledAsync(testUser, testLock);
            await userStore.SetLockoutEndDateAsync(testUser, testEnd);

            await userStore.UpdateAsync(testUser);
            testUser = await FindTestUserAsync();

            var curCount = await userStore.GetAccessFailedCountAsync(testUser);
            var curLock = await userStore.GetLockoutEnabledAsync(testUser);
            var curEnd = await userStore.GetLockoutEndDateAsync(testUser);

            Assert.AreEqual(testCount + 1, curCount, "count was incremented");
            Assert.AreEqual(testLock, curLock, "Lock out was changed");
            Assert.AreEqual(testEnd, curEnd, " was changed");

            await userStore.ResetAccessFailedCountAsync(testUser);
            await userStore.UpdateAsync(testUser);
            testUser = await FindTestUserAsync();
            
            curCount = await userStore.GetAccessFailedCountAsync(testUser);
            Assert.AreEqual(0, curCount, "count was reset");
        }

        // IUserRoleStore

        private async Task<IdentityRole> FindTestRoleAsync()
        {
            var testRole = await roleStore.FindByNameAsync(testRoleName);
            if (testRole == null)
            {
                var newRole = new IdentityRole(testRoleName);
                await roleStore.CreateAsync(newRole);
                testRole = await roleStore.FindByNameAsync(testRoleName);
            }
            return testRole;
        }


        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException), "Invalid role name was allowed")]
        public async Task User_role_raise_on_bad_role_name()
        {
            var testUser = await FindTestUserAsync();
            await userStore.AddToRoleAsync(testUser, "_99_-A%%bad1234_name");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Invalid role name was allowed")]
        public async Task User_role_raise_on_null_role_name()
        {
            var testUser = await FindTestUserAsync();
            await userStore.AddToRoleAsync(testUser, null);
        }
        

        [TestMethod]
        public async Task User_role_store()
        {
            var testUser = await FindTestUserAsync();
            var testRole = await FindTestRoleAsync();
            var testRoleName = testRole.Name;

            var inRole = await userStore.IsInRoleAsync(testUser, testRoleName);
            if (inRole)
            {
                await userStore.RemoveFromRoleAsync(testUser, testRoleName);
                await userStore.UpdateAsync(testUser);
                inRole = await userStore.IsInRoleAsync(testUser, testRoleName);
                Assert.IsFalse(inRole, "Removed from test role");

                await userStore.AddToRoleAsync(testUser, testRoleName);
                await userStore.UpdateAsync(testUser);
                inRole = await userStore.IsInRoleAsync(testUser, testRoleName);
                Assert.IsTrue(inRole, "Added to test role");
            }
            else
            {
                await userStore.AddToRoleAsync(testUser, testRoleName);
                await userStore.UpdateAsync(testUser);
                inRole = await userStore.IsInRoleAsync(testUser, testRoleName);
                Assert.IsTrue(inRole, "Added to test role");

                await userStore.RemoveFromRoleAsync(testUser, testRoleName);
                await userStore.UpdateAsync(testUser);
                inRole = await userStore.IsInRoleAsync(testUser, testRoleName);
                Assert.IsFalse(inRole, "Removed from test role");
            }
        }

        // IQueryableRoleStore

        [TestMethod]
        public void RoleStore_roles_is_Queryable_of_IdentityUser()
        {
            var roles = roleStore.Roles;
            Assert.IsInstanceOfType(roles, typeof(IQueryable<IdentityRole>));
        }

        // Tests for IRoleStore

        [TestMethod]
        public async Task RoleStore_Create_adds_1_role()
        {
            var testRole = FindTestRoleByName();
            if (testRole != null)
            {
                context.Roles.Remove(testRole);
                context.SaveChanges();
            }

            int orig = roleStore.Roles.Count();

            var role = new IdentityRole(testRoleName);
            var newId = role.Id;

            await roleStore.CreateAsync(role);

            int added = roleStore.Roles.Count();

            Assert.AreEqual(added, orig + 1, "One Role was added");

            var found = await roleStore.FindByIdAsync(newId);

            Assert.IsInstanceOfType(found, typeof(IdentityRole), "Found new IdentityRole for new Id");

            await roleStore.DeleteAsync(found);

            int deleted = roleStore.Roles.Count();

            Assert.AreEqual(deleted, added - 1, "One Role was removed");

            found = await roleStore.FindByIdAsync(newId);

            Assert.IsNull(found, "New Role should not be found by Id");
        }

        [TestMethod]
        public async Task RoleStore_Update()
        {
            var testRole = await FindTestRoleAsync();
            
            Assert.IsInstanceOfType(testRole, typeof(IdentityRole), "Found test IdentityRole for testName");

            string changedName = "changed_name";

            var changedRole = await roleStore.FindByNameAsync(changedName);
            if (changedRole != null)
            {
                await roleStore.DeleteAsync(changedRole); 
            }
            
            testRole.Name = changedName;
            await roleStore.UpdateAsync(testRole);

            testRole = await roleStore.FindByNameAsync(changedName);
            Assert.IsInstanceOfType(testRole, typeof(IdentityRole), "Found test IdentityRole for changedName");

            await roleStore.DeleteAsync(testRole);

            testRole = await roleStore.FindByNameAsync(changedName);
            Assert.IsNull(testRole, "Test role with changed name removed");

        }

    }
}
