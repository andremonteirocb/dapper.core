using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dapper.Core.Interfaces
{
    public interface IRepository<TEntity> : IRepositoryTransaction where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetPagedAsync(int pageNumber, int pageSize);
        Task<TEntity> GetAsync(object id);
        Task<TReturn> GetAsync<T1, T2, TReturn>(object id) where T1 : class, TReturn;
        Task<TReturn> GetAsync<T1, T2, T3, TReturn>(object id) where T1 : class, TReturn;
        Task<TReturn> GetAsync<T1, T2, T3, T4, TReturn>(object id) where T1 : class, TReturn;
        Task<TReturn> GetAsync<T1, T2, T3, T4, T5, TReturn>(object id) where T1 : class, TReturn;
        Task<TReturn> GetAsync<T1, T2, T3, T4, T5, T6, TReturn>(object id) where T1 : class, TReturn;
        Task<TReturn> GetAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(object id) where T1 : class, TReturn;

        Task SaveAsync(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task SaveInListAsync(IEnumerable<TEntity> entities);

        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteMultipleAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> QueryAsync(string sql, CommandType commandType = CommandType.Text, object parameters = null);
        Task<TEntity> QueryFirstOrDefaultAsync(string sql, CommandType commandType = CommandType.Text, object parameters = null);

        Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SelectFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task AlterarExclusao(Guid id);
        Task AtivaDesativa(object id, string coluna);

        Task<IEnumerable<TReturn>> MultiMapAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map, CommandType commandType = CommandType.Text, object parameters = null, string splitOn = "Id") where T1 : class, TReturn;
        Task<IEnumerable<TReturn>> MultiMapAsync<T1, T2, T3, TReturn>(string sql, Func<T1, T2, T3, TReturn> map, CommandType commandType = CommandType.Text, object parameters = null, string splitOn = "Id") where T1 : class, TReturn;
        Task<IEnumerable<TReturn>> MultiMapAsync<T1, T2, T3, T4, TReturn>(string sql, Func<T1, T2, T3, T4, TReturn> map, CommandType commandType = CommandType.Text, object parameters = null, string splitOn = "Id") where T1 : class, TReturn;
        Task<IEnumerable<TReturn>> MultiMapAsync<T1, T2, T3, T4, T5, TReturn>(string sql, Func<T1, T2, T3, T4, T5, TReturn> map, CommandType commandType = CommandType.Text, object parameters = null, string splitOn = "Id") where T1 : class, TReturn;
        Task<IEnumerable<TReturn>> MultiMapAsync<T1, T2, T3, T4, T5, T6, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, TReturn> map, CommandType commandType = CommandType.Text, object parameters = null, string splitOn = "Id") where T1 : class, TReturn;
        Task<IEnumerable<TReturn>> MultiMapAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, CommandType commandType = CommandType.Text, object parameters = null, string splitOn = "Id") where T1 : class, TReturn;
        Task<SqlMapper.GridReader> ExecuteMultiply(string sql, CommandType commandType = CommandType.Text, object parameters = null);
    }
}
