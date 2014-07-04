using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Identity.Oracle
{
    public static class DataHelper
    {
        public const string BoolYNTue = "Y";
        public const string BoolYNFalse = "N";

        public static bool BoolYN(string flag)
        {
            return ((flag != null) && flag.Equals(BoolYNTue));
        }

        public static string BoolYNStr(bool active)
        {
            if (active)
            { return BoolYNTue; }
            else
            { return BoolYNFalse; }
        }
    }

    public partial class AspIdentity
    {
        // wrap sync call with async wait
        public async Task<TResult> WrapWait<TResult>(Func<TResult> function)
        {
            return await Task<TResult>.Run(function);
        }

        // entity framework 5 Async save
        public async Task<int> SaveEF5ChangesAsync()
        {
            return await Task.Run(() => this.SaveChanges());
        }

    }
}
