using System;
using System.Linq;
using System.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace Asp.Identity.Oracle.Test
{
    [TestClass]
    public class UnitTest1
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

        private async Task<IdentityUser> FindTestUser()
        {
            var found = await userStore.FindByNameAsync(testUserName);
            if (found == null)
            {
                var user = new IdentityUser(testUserName);
                await userStore.CreateAsync(user);
                found = await userStore.FindByNameAsync(testUserName);
            }
            return found;
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Users with duplicate names were allowed")]
        public async Task Dont_add_duplicate_named_users()
        {
            var testUser = await FindTestUser();
            var newUser = new IdentityUser(testUser.UserName);
            await userStore.CreateAsync(newUser);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Duplicate Users without names were allowed")]
        public async Task Dont_add_duplicate_no_named_users()
        {
            var newUser = new IdentityUser();
            await userStore.CreateAsync(newUser);
            var nextUser = new IdentityUser();
            await userStore.CreateAsync(nextUser);
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
            int orig = userStore.Users.Count();

            var user = new IdentityUser();
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
            var found = await FindTestUser();
            Assert.IsInstanceOfType(found, typeof(IdentityUser), "Found new IdentityUser for new Id");

            Assert.AreEqual(testUserName, found.UserName, "Found user has test user name");

            string name = found.UserName;

            string email;
            if (found.Email == "bobs@your.uncle") {
                email = "bobs@notyour.uncle";
            } else {
                email = "bobs@your.uncle";
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
            var user = await FindTestUser();

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
            var testUser = await FindTestUser();
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
            var testUser = await FindTestUser();
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
            var testUser = await FindTestUser();

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
            
            var testUser = await FindTestUser();
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
            var testUser = await FindTestUser();

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

            testUser = await FindTestUser();
            var curPhone = await userStore.GetPhoneNumberAsync(testUser);
            var curConf = await userStore.GetPhoneNumberConfirmedAsync(testUser);

            Assert.AreEqual(testPhone, curPhone, "GetPhoneNumberAsync phone matches that set");
            Assert.AreEqual(testConf, curConf, "phone confirmed was changed");
            
        }

        // IUserTwoFactorStore

        [TestMethod]
        public async Task User_twofactor_store_asyncs()
        {
            var testUser = await FindTestUser();
            var test2fact = !await userStore.GetTwoFactorEnabledAsync(testUser);

            await userStore.SetTwoFactorEnabledAsync(testUser, test2fact);
            await userStore.UpdateAsync(testUser);

            testUser = await FindTestUser();
            var cur2Fact = await userStore.GetTwoFactorEnabledAsync(testUser);
            Assert.AreEqual(test2fact, cur2Fact, "two factor enabled was changed");
        }

        // IUserLockoutStore

        [TestMethod]
        public async Task User_lockout_asyncs()
        {
            var testUser = await FindTestUser();
            var testCount = await userStore.GetAccessFailedCountAsync(testUser);
            var testLock = !await userStore.GetLockoutEnabledAsync(testUser);
            var end = await userStore.GetLockoutEndDateAsync(testUser);
            var testEnd = end.AddDays(1);

            await userStore.IncrementAccessFailedCountAsync(testUser);
            await userStore.SetLockoutEnabledAsync(testUser, testLock);
            await userStore.SetLockoutEndDateAsync(testUser, testEnd);

            await userStore.UpdateAsync(testUser);
            testUser = await FindTestUser();

            var curCount = await userStore.GetAccessFailedCountAsync(testUser);
            var curLock = await userStore.GetLockoutEnabledAsync(testUser);
            var curEnd = await userStore.GetLockoutEndDateAsync(testUser);

            Assert.AreEqual(testCount + 1, curCount, "count was incremented");
            Assert.AreEqual(testLock, curLock, "Lock out was changed");
            Assert.AreEqual(testEnd, curEnd, " was changed");

            await userStore.ResetAccessFailedCountAsync(testUser);
            await userStore.UpdateAsync(testUser);
            testUser = await FindTestUser();
            
            curCount = await userStore.GetAccessFailedCountAsync(testUser);
            Assert.AreEqual(0, curCount, "count was reset");
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
            int orig = roleStore.Roles.Count();

            var role = new IdentityRole();
            var newId = role.Id;

            await roleStore.CreateAsync(role);

            //int added = roleStore.Roles.Count();

            //Assert.AreEqual(added, orig + 1, "One Role was added");

            //var found = await roleStore.FindByIdAsync(newId);

            //Assert.IsInstanceOfType(found, typeof(IdentityRole), "Found new IdentityRole for new Id");

            //await roleStore.DeleteAsync(found);

            //int deleted = roleStore.Roles.Count();

            //Assert.AreEqual(deleted, added - 1, "One Role was removed");

            //found = await roleStore.FindByIdAsync(newId);

            //Assert.IsNull(found, "New Role should not be found by Id");
        }


        //[TestMethod]
        //public async Task User_roles_asyncs()        {        }

        [TestMethod]
        public void TestMethod1()
        {
            var user = new IdentityUser { 
                Id = Guid.NewGuid().ToString()
            };
            
            var x = from u in context.Users
                    where u.UserName == user.UserName
                    select u;

            var l = x.ToList();
            

            //var l = x.ToList();
            //var appContext = new ApplicationDbContext();
            //var store = new UserStore<ApplicationUser>(appContext);
            //var manager = new ApplicationUserManager(store);
        }

    }
}
