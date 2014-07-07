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
        public async Task UserStore_Create_adds_user()
        {
            var user = new IdentityUser();
            await store.CreateAsync(user);

            var users = store.Users;
        }



        [TestMethod]
        public void TestMethod1()
        {
            var user = new IdentityUser();
            
            var x = from u in context.Users
                    where u.UserName == user.UserName
                    select u;
            var l = x.ToList();
            
            if (context.Users.Any(u => String.Equals(u.UserName, user.UserName)))
            { 
            }

            //var l = x.ToList();
            //var appContext = new ApplicationDbContext();
            //var store = new UserStore<ApplicationUser>(appContext);
            //var manager = new ApplicationUserManager(store);
        }
    }
}
