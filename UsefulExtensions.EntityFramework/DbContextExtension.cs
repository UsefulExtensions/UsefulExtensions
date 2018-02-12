using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

using UsefulExtensions.List;
using UsefulExtensions.Reflection;

namespace UsefulExtensions.EntityFramework
{
    public static class DbContextExtension
    {
        public static DbContext GetContext<TEntity>(this IDbSet<TEntity> dbSet)
            where TEntity : class
        {
            object internalSet = dbSet
                .GetType()
                .GetField("_internalSet", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(dbSet);
            object internalContext = internalSet
                .GetType()
                .BaseType
                .GetField("_internalContext", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(internalSet);
            return (DbContext)internalContext
                .GetType()
                .GetProperty("Owner", BindingFlags.Instance | BindingFlags.Public)
                .GetValue(internalContext, null);
        }

        public static void BulkInsert<T>(this DbContext context, IList<T> entityList, int batchSize = 1000) where T : class
        {
            string connectionString = context.Database.Connection.ConnectionString;

            DataTable dataTable = entityList.ToDataTable();

            string tableName = entityList.First().GetType().GetAttributeValue((TableAttribute dna) => dna.Name);

            if (string.IsNullOrEmpty(tableName))
            {
                tableName = entityList.First().GetType().Name;
            }

            using (var sqlBulk = new SqlBulkCopy(connectionString))
            {
                sqlBulk.BatchSize = batchSize;
                sqlBulk.DestinationTableName = tableName;
                sqlBulk.WriteToServer(dataTable);
            }
        }
    }
}