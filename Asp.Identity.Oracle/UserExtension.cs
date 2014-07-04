using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Identity.Oracle
{
    public partial class User
    {
        public bool EmailConfirmed
        {
            get { return DataHelper.BoolYN(EmailConfirmedFlag); }
            set
            {
                EmailConfirmedFlag = DataHelper.BoolYNStr(value);
            }
        }

        public bool PhoneNumberConfirmed
        {
            get { return DataHelper.BoolYN(PhoneNumberConfirmedFlag); }
            set
            {
                PhoneNumberConfirmedFlag = DataHelper.BoolYNStr(value);
            }
        }

        public bool TwoFactorEnabled
        {
            get { return DataHelper.BoolYN(TwoFactorEnabledFlag); }
            set
            {
                TwoFactorEnabledFlag = DataHelper.BoolYNStr(value);
            }
        }

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
