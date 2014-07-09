using System;
using Microsoft.AspNet.Identity;

namespace Asp.Identity.Oracle
{
    public abstract class IdentityRoleInit
    {
        protected IdentityRoleInit()
        {
            Initialize();
        }

        protected abstract void Initialize();
    }

    public partial class IdentityRole : IdentityRoleInit, IRole<string>
    {
        protected override void Initialize()
        {
            Id = Guid.NewGuid().ToString();
        }

        public IdentityRole(string name) : this()
        {
            Name = name;
        }
    }
}
