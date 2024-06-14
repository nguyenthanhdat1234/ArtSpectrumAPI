using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ArtSpectrum.Extendsions
{
    public static class DbContextExtensions
    {
        public static IQueryable<T> OrderByIdDescending<T>(this DbSet<T> dbSet) where T : class
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            var property = typeof(T).GetProperties()
                                    .FirstOrDefault(p => p.Name.ToLower().Contains("id"));

            if (property == null)
            {
                throw new InvalidOperationException("No ID property found for type " + typeof(T).Name);
            }

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var resultExp = Expression.Call(
                typeof(Queryable),
                "OrderByDescending",
                new Type[] { typeof(T), property.PropertyType },
                dbSet.AsQueryable().Expression,
                Expression.Quote(orderByExp)
            );

            return dbSet.AsQueryable().Provider.CreateQuery<T>(resultExp);
        }
    }
}
