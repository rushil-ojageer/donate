using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Donate.Shared.Data.Extensions
{
    public static class DeletableExtensions
    {
        public static IQueryable<T> FilterDeletedItems<T>(this DbSet<T> dbSet) where T : class, IDeletableEntity
        {
            return dbSet.Where(x => !x.IsDeleted);
        }
    }
}
