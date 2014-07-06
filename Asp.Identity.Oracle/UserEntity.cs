using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Asp.Identity.Oracle
{
    //public partial class IdentityUser : IUser<string>    {    }

    /// <summary>
    ///     Default EntityFramework IUser implementation
    /// </summary>
    public partial class IdentityUser : IUser<string>
    {
        /// <summary>
        ///     Constructor which creates a new Guid for the Id
        /// </summary>
        //public IdentityUser()
        //{
        //    Id = Guid.NewGuid().ToString();
        //}

        /// <summary>
        ///     Constructor that takes a userName
        /// </summary>
        /// <param name="userName"></param>
        public IdentityUser(string userName)
            : this()
        {
            UserName = userName;
        }

        /// <summary>
        ///     True if the email is confirmed, default is false
        /// </summary>        
        public bool EmailConfirmed
        {
            get { return DataHelper.BoolYN(EmailConfirmedFlag); }
            set
            {
                EmailConfirmedFlag = DataHelper.BoolYNStr(value);
            }
        }

        /// <summary>
        ///     True if the phone number is confirmed, default is false
        /// </summary>
        public bool PhoneNumberConfirmed
        {
            get { return DataHelper.BoolYN(PhoneNumberConfirmedFlag); }
            set
            {
                PhoneNumberConfirmedFlag = DataHelper.BoolYNStr(value);
            }
        }

        /// <summary>
        ///     Is two factor enabled for the user
        /// </summary>
        public bool TwoFactorEnabled
        {
            get { return DataHelper.BoolYN(TwoFactorEnabledFlag); }
            set
            {
                TwoFactorEnabledFlag = DataHelper.BoolYNStr(value);
            }
        }

        /// <summary>
        ///     Is lockout enabled for this user
        /// </summary>
        public bool LockoutEnabled
        {
            get { return DataHelper.BoolYN(LockoutEnabledFlag); }
            set
            {
                LockoutEnabledFlag = DataHelper.BoolYNStr(value);
            }
        }

    }

}
