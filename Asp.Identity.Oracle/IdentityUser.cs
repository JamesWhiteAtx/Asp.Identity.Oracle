//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Asp.Identity.Oracle
{
    using System;
    using System.Collections.Generic;
    
    public partial class IdentityUser
    {
        public IdentityUser()
        {
            this.Claims = new HashSet<IdentityUserClaim>();
            this.Logins = new HashSet<IdentityUserLogin>();
            this.Roles = new HashSet<IdentityRole>();
        }
    
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string Email { get; set; }
        public string EmailConfirmedFlag { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberConfirmedFlag { get; set; }
        public string TwoFactorEnabledFlag { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public string LockoutEnabledFlag { get; set; }
        public int AccessFailedCount { get; set; }
    
        public virtual ICollection<IdentityUserClaim> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin> Logins { get; set; }
        public virtual ICollection<IdentityRole> Roles { get; set; }
    }
}
