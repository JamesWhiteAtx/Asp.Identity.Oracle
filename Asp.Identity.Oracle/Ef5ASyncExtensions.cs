using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Identity.Oracle
{
    public static class Ef5ASyncExtensions
    {
        public static Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> source)
        {
            return Task<List<TSource>>.Run(() => source.ToList());
        }
    }

}
