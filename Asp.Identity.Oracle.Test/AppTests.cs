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

    public class XUser : IdentityUser
    { }

    [TestClass]
    public class AppTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            //ApplicationDbContext context = new ApplicationDbContext();
            
            //var userStore = new UserStore<ApplicationUser>(context);

            //var idUsers = context.Users;
            //IQueryable<IdentityUser> idUsers = new List<IdentityUser>();
            //IQueryable<ApplicationUser> appUsers = idUsers;

        }
    }
}

//Unable to cast object of type System.Data.Entity.DbSet to type System.Linq.IQueryable