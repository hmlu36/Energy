using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Energy.Utils
{
    public static class DbContextExtensions
    {
        // 使用字串取得 DbSet
        public static DbSet<TEntity> GetDbSet<TEntity>(this DbContext dbContext, string tableName)
        where TEntity : class
        {
            var dbSetProperty = dbContext.GetType().GetProperty(tableName);
            if (dbSetProperty == null)
            {
                throw new ArgumentException($"Table {tableName} does not exist in DbContext.");
            }
            var dbSet = (DbSet<TEntity>)dbSetProperty.GetValue(dbContext);
            return dbSet;
        }

        public static object CreateDbSetInstance(this DbContext context, string entityName)
        {
            var entityType = context.GetType().Assembly.GetTypes().FirstOrDefault(t => t.Name == entityName);
            if (entityType == null)
            {
                throw new ArgumentException($"Cannot find entity type '{entityName}' in context '{context.GetType().Name}'.");
            }

            var dbSetType = typeof(DbSet<>).MakeGenericType(entityType);
            return context.GetType().GetMethod("Set", new Type[] { }).MakeGenericMethod(entityType).Invoke(context, null);
        }
    }
}
