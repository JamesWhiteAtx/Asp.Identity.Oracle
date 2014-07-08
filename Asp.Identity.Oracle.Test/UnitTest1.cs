using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Asp.Identity.Oracle.Test
{
    [TestClass]
    public class UnitTest1
    {
        IdentityDbContext context;
        UserStore store;

        [TestInitialize]
        public void Setup()
        {
            context = new IdentityDbContext();
            store = new UserStore(context);
        }

        public void Cleaup()
        {
            store.Dispose();
            context.Dispose();
        }

        [TestMethod]
        public void UserStore_Users_is_Queryable_of_IdentityUser()
        {
            var users = store.Users;
            Assert.IsInstanceOfType(users, typeof(IQueryable<IdentityUser>));
        }

        [TestMethod]
        public async Task UserStore_Create_adds_1_user()
        {
            int orig = store.Users.Count();

            //var newId = Guid.NewGuid().ToString();
            var user = new IdentityUser(); //           {                Id = newId            };
            var newId = user.Id;
           
            await store.CreateAsync(user);

            int added = store.Users.Count();
            
            Assert.AreEqual(added, orig+1, "One User was added");

            var found = await store.FindByIdAsync(newId);

            Assert.IsInstanceOfType(found, typeof(IdentityUser), "Found new IdentityUser for new Id");

            await store.DeleteAsync(found);

            int deleted = store.Users.Count();

            Assert.AreEqual(deleted, added - 1, "One User was removed");

            found = await store.FindByIdAsync(newId);

            Assert.IsNull(found, "New User cannot be found by Id");
        }

        [TestMethod]
        public void Identity_resource()
        {
            //var s = IdentityResources.DuplicateUserName;
            //Assert.IsInstanceOfType(s, typeof(string));
        }

        [TestMethod]
        public async Task UserStore_find_by_name()
        {
            string name = "test_user";
            var found = await store.FindByIdAsync(name);
            if (found == null) {
                var user = new IdentityUser(name);
                await store.CreateAsync(user);
                found = await store.FindByIdAsync(name);
            }
            Assert.IsInstanceOfType(found, typeof(IdentityUser), "Found new IdentityUser for new Id");

        }

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
