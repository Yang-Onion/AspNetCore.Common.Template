using AspNetCore.Common.Infrastructure.Interface;
using AspNetCore.Common.Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Common.Infrastructure.Data
{
    public abstract class DataDbContext<IDbContext> : DbContext, IUnitOfWork where IDbContext : DbContext
    {
        private readonly string _connectionString;

        public DataDbContext()
        {
        }

        public DataDbContext(DbContextOptions<IDbContext> options, string connectionString)
            : base(options)
        {
            _connectionString = connectionString;
        }

        public int Commit()
        {
            try
            {
                return SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().OriginalValues.SetValues(ex.Entries.Single().GetDatabaseValues());
                return SaveChanges();
            }
        }

        public Task<int> CommitAsync()
        {
            try
            {
                return SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().OriginalValues.SetValues(ex.Entries.Single().GetDatabaseValues());
                return SaveChangesAsync();
            }
        }

        public PagedList<T> Sql<T>(string sql, string orderColumn, PageQuery pager)
        {
            using (Database.GetService<IConcurrencyDetector>().EnterCriticalSection())
            {
                var totalRows = 0;
                var executeSql = sql;
                var totalSql = string.Format("select count(1) from ({0}) t", sql);
                var totalCommand = Database.GetService<IRawSqlCommandBuilder>().Build(totalSql, new List<object>());
                totalRows =
                    (int)
                    totalCommand.RelationalCommand.ExecuteScalar(Database.GetService<IRelationalConnection>(),
                        totalCommand.ParameterValues);
                if (pager.IsPage)
                {
                    pager.PageSize = pager.PageSize == 0 ? 20 : pager.PageSize;
                    executeSql =
                        string.Format("select * from ({0}) lt order by {3} OFFSET {1} ROW  FETCH NEXT {2} ROW ONLY",
                            sql,
                            pager.PageIndex * pager.PageSize, pager.PageSize, orderColumn);
                }
                var command = Database.GetService<IRawSqlCommandBuilder>().Build(executeSql, new List<object>());
                var reader = command.RelationalCommand.ExecuteReader(Database.GetService<IRelationalConnection>(),
                    command.ParameterValues);
                var properties = typeof(T).GetProperties().ToList();
                IList<T> result = new List<T>();
                while (reader.DbDataReader.Read())
                {
                    var newT = Activator.CreateInstance<T>();
                    for (var i = 0; i < reader.DbDataReader.FieldCount; i++)
                    {
                        var prop =
                            properties.FirstOrDefault(
                                g => g.Name.Equals(reader.DbDataReader.GetName(i), StringComparison.OrdinalIgnoreCase));
                        if (prop != null)
                            if (!reader.DbDataReader.IsDBNull(i))
                                prop.SetValue(newT, reader.DbDataReader.GetValue(i));
                    }
                    result.Add(newT);
                }
                return new PagedList<T>(result)
                {
                    Paged =
                        new Pagination.Pagination
                        {
                            PageIndex = pager.PageIndex,
                            PageSize = pager.PageSize,
                            PageCount = (int) Math.Ceiling(totalRows / (pager.PageSize * 1.0)),
                            TotalCount = totalRows
                        }
                };
            }
        }

        public IList<T> Sql<T>(string sql)
        {
            using (Database.GetService<IConcurrencyDetector>().EnterCriticalSection())
            {
                var buildSqlCommand = Database.GetService<IRawSqlCommandBuilder>().Build(sql, new List<object>());
                var reader =
                    buildSqlCommand.RelationalCommand.ExecuteReader(Database.GetService<IRelationalConnection>(),
                        buildSqlCommand.ParameterValues);
                var properties = typeof(T).GetProperties().ToList();
                IList<T> result = new List<T>();
                while (reader.DbDataReader.Read())
                {
                    var newT = Activator.CreateInstance<T>();
                    for (var i = 0; i < reader.DbDataReader.FieldCount; i++)
                    {
                        var prop =
                            properties.FirstOrDefault(
                                g => g.Name.Equals(reader.DbDataReader.GetName(i), StringComparison.OrdinalIgnoreCase));
                        if (prop != null)
                            if (!reader.DbDataReader.IsDBNull(i))
                                prop.SetValue(newT, reader.DbDataReader.GetValue(i));
                    }
                    result.Add(newT);
                }
                return result;
            }
        }

        public T ExecuteScalar<T>(string sql)
        {
            using (Database.GetService<IConcurrencyDetector>().EnterCriticalSection())
            {
                var command = Database.GetService<IRawSqlCommandBuilder>().Build(sql, new List<object>());
                var obj = command.RelationalCommand.ExecuteScalar(Database.GetService<IRelationalConnection>(),
                    command.ParameterValues);
                try
                {
                    return (T) Convert.ChangeType(obj, typeof(T));
                }
                catch(Exception ex)
                {
                    try
                    {
                        return (T) obj;
                    }
                    catch
                    {
                        return default(T);
                    }
                }
            }
        }

        /// <summary>
        /// SQL分页 sql语句中包括CTE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cte"></param>
        /// <param name="sql"></param>
        /// <param name="orderColumn"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public PagedList<T> SqlCTE<T>(string cteSql, string sql, string orderColumn, PageQuery pager,
            SqlParameter[] parameters = null)
        {
            using (Database.GetService<IConcurrencyDetector>().EnterCriticalSection())
            {
                var totalRows = 0;
                var executeSql = sql;
                var totalSql = cteSql;
                var totalCommand = Database.GetService<IRawSqlCommandBuilder>().Build(totalSql,
                    parameters == null ? new List<SqlParameter>() : parameters.ToList());
                totalRows =
                    (int)
                    totalCommand.RelationalCommand.ExecuteScalar(Database.GetService<IRelationalConnection>(),
                        totalCommand.ParameterValues);
                if (pager.IsPage)
                {
                    pager.PageSize = pager.PageSize == 0 ? 20 : pager.PageSize;
                    executeSql =
                        string.Format("{0} order by {3} OFFSET {1} ROW  FETCH NEXT {2} ROW ONLY", sql,
                            pager.PageIndex * pager.PageSize, pager.PageSize, orderColumn);
                }
                var command = Database.GetService<IRawSqlCommandBuilder>().Build(executeSql,
                    parameters == null ? new List<SqlParameter>() : parameters.ToList());
                var reader = command.RelationalCommand.ExecuteReader(Database.GetService<IRelationalConnection>(),
                    command.ParameterValues);
                var properties = typeof(T).GetProperties().ToList();
                IList<T> result = new List<T>();
                while (reader.DbDataReader.Read())
                {
                    var newT = Activator.CreateInstance<T>();
                    for (var i = 0; i < reader.DbDataReader.FieldCount; i++)
                    {
                        var prop =
                            properties.FirstOrDefault(
                                g => g.Name.Equals(reader.DbDataReader.GetName(i), StringComparison.OrdinalIgnoreCase));
                        if (prop != null)
                            if (!reader.DbDataReader.IsDBNull(i))
                                if (prop.PropertyType.Name.Equals("Decimal"))
                                    prop.SetValue(newT, Convert.ToDecimal(reader.DbDataReader.GetValue(i)));
                                else
                                    prop.SetValue(newT, reader.DbDataReader.GetValue(i));
                    }
                    result.Add(newT);
                }
                return new PagedList<T>(result)
                {
                    Paged =
                        new Pagination.Pagination
                        {
                            PageIndex = pager.PageIndex,
                            PageSize = pager.PageSize,
                            PageCount = (int) Math.Ceiling(totalRows / (pager.PageSize * 1.0)),
                            TotalCount = totalRows
                        }
                };
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            optionsBuilder.ConfigureWarnings(x => { x.Ignore(); });

            base.OnConfiguring(optionsBuilder);
        }
    }
}