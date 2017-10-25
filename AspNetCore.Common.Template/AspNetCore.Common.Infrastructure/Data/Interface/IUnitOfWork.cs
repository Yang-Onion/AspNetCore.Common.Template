using AspNetCore.Common.Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Common.Infrastructure.Interface
{
    public interface IUnitOfWork
    {
        DatabaseFacade Database { get; }

        int Commit();

        Task<int> CommitAsync();

        DbSet<T> Set<T>() where T : class;

        //PagedList<T> Sql<T>(string sql, string orderColumn, PageQuery pager);

        //PagedList<T> SqlCTE<T>(string CTESql, string sql, string orderColumn, PageQuery pager, SqlParameter[] parameters = null);

        IList<T> Sql<T>(string sql);

        T ExecuteScalar<T>(string sql);
    }
}
