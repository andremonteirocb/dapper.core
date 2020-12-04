using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq.Expressions;
using Dapper;
using Dapper.Core.Interfaces;
using System.Linq;
using Dommel;
using Luma.Dommel;
using Dapper.Core.Base;
using Dapper.Core.Extensions;

namespace Dapper.Core
{
    public class Repository<TEntity> : DataBaseContext, IRepository<TEntity> where TEntity : class
    {
        public Repository()
        {
            DommelMapper.SetTableNameResolver(new DefaultTableNameResolver());
            DommelMapper.SetKeyPropertyResolver(new DefaultKeyPropertyResolver());
            DommelMapper.SetColumnNameResolver(new DefaultColumnNameResolver());
            DommelMapper.SetPropertyResolver(new DefaultPropertyResolver());
            DommelMapper.SetForeignKeyPropertyResolver(new DefaultForeignKeyPropertyResolver());
        }

        public Repository(IDbTransaction transaction)
        {
            base.SetTransaction(transaction);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.GetAllAsync<TEntity>(base._dbTransaction);
            else
            {
                using (connection)
                    return await connection.GetAllAsync<TEntity>();
            }
        }

        public async Task<IEnumerable<TEntity>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.GetPagedAsync<TEntity>(pageNumber, pageSize, base._dbTransaction);
            else
            {
                using (connection)
                    return await connection.GetPagedAsync<TEntity>(pageNumber, pageSize);
            }
        }

        public async Task<TEntity> GetAsync(object id)
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.GetAsync<TEntity>(id, base._dbTransaction);
            else
            {
                using (connection)
                    return await connection.GetAsync<TEntity>(id);
            }
        }

        public async Task<TReturn> GetAsync<T1, T2, TReturn>(object id) where T1 : class, TReturn
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.GetAsync<T1, T2, TReturn>(id, base._dbTransaction);
            else
            {
                using (connection)
                    return await connection.GetAsync<T1, T2, TReturn>(id);
            }
        }

        public async Task<TReturn> GetAsync<T1, T2, T3, TReturn>(object id) where T1 : class, TReturn
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.GetAsync<T1, T2, T3, TReturn>(id, base._dbTransaction);
            else
            {
                using (connection)
                    return await connection.GetAsync<T1, T2, T3, TReturn>(id);
            }
        }

        public async Task<TReturn> GetAsync<T1, T2, T3, T4, TReturn>(object id) where T1 : class, TReturn
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.GetAsync<T1, T2, T3, T4, TReturn>(id, base._dbTransaction);
            else
            {
                using (connection)
                    return await connection.GetAsync<T1, T2, T3, T4, TReturn>(id);
            }
        }

        public async Task<TReturn> GetAsync<T1, T2, T3, T4, T5, TReturn>(object id) where T1 : class, TReturn
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.GetAsync<T1, T2, T3, T4, T5, TReturn>(id, base._dbTransaction);
            else
            {
                using (connection)
                    return await connection.GetAsync<T1, T2, T3, T4, T5, TReturn>(id);
            }
        }

        public async Task<TReturn> GetAsync<T1, T2, T3, T4, T5, T6, TReturn>(object id) where T1 : class, TReturn
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.GetAsync<T1, T2, T3, T4, T5, T6, TReturn>(id, base._dbTransaction);
            else
            {
                using (connection)
                    return await connection.GetAsync<T1, T2, T3, T4, T5, T6, TReturn>(id);
            }
        }

        public async Task<TReturn> GetAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(object id) where T1 : class, TReturn
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.GetAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(id, base._dbTransaction);
            else
            {
                using (connection)
                    return await connection.GetAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(id);
            }
        }

        public async Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.SelectAsync<TEntity>(predicate, base._dbTransaction);
            else
            {
                using (connection)
                    return await connection.SelectAsync<TEntity>(predicate);
            }
        }

        public async Task<TEntity> SelectFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return (await connection.SelectAsync<TEntity>(predicate, base._dbTransaction))?.FirstOrDefault();
            else
            {
                using (connection)
                    return (await connection.SelectAsync<TEntity>(predicate))?.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<TEntity>> QueryAsync(string sql, CommandType commandType = CommandType.Text, object parameters = null)
        {
            var connection = base.GetConnection();

            if (base.ContextoDeTransacao)
                return await connection.QueryAsync<TEntity>(sql, parameters, base._dbTransaction, commandType: commandType);
            else
            {
                using (connection)
                    return await connection.QueryAsync<TEntity>(sql, parameters, commandType: commandType);
            }
        }

        public async Task<TEntity> QueryFirstOrDefaultAsync(string sql, CommandType commandType = CommandType.Text, object parameters = null)
        {
            var connection = base.GetConnection();

            if (base.ContextoDeTransacao)
                return await connection.QueryFirstOrDefaultAsync<TEntity>(sql, parameters, base._dbTransaction, commandType: commandType);
            else
            {
                using (connection)
                    return await connection.QueryFirstOrDefaultAsync<TEntity>(sql, parameters, commandType: commandType);
            }
        }

        public async Task SaveAsync(TEntity entity)
        {
            if (ExistValueKey(entity))
                await UpdateAsync(entity);
            else
                await InsertAsync(entity);
        }

        public async Task SaveInListAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                await SaveAsync(entity);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            ExistValueKey(entity);
            SetDate(entity);

            var connection = base.GetConnection();
            object value = null;

            if (base.ContextoDeTransacao)
                value = (await connection.InsertAsync<TEntity>(entity, base._dbTransaction));
            else
            {
                using (connection)
                    value = (await connection.InsertAsync<TEntity>(entity));
            }

            if (value != null)
                SetValueKey(entity, value);

            return entity;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            SetDate(entity);

            var connection = base.GetConnection();

            if (base.ContextoDeTransacao)
                return await connection.UpdateAsync<TEntity>(entity, base._dbTransaction);
            else
            {
                using (connection)
                    return await connection.UpdateAsync<TEntity>(entity);
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            var connection = base.GetConnection();

            if (base.ContextoDeTransacao)
                return await connection.DeleteAsync<TEntity>(entity, base._dbTransaction);
            else
            {
                using (connection)
                    return await connection.DeleteAsync<TEntity>(entity);
            }
        }

        public async Task<bool> DeleteMultipleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var connection = base.GetConnection();

            if (base.ContextoDeTransacao)
                return await connection.DeleteMultipleAsync<TEntity>(predicate, base._dbTransaction);
            else
            {
                using (connection)
                    return await connection.DeleteMultipleAsync<TEntity>(predicate);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var connection = base.GetConnection();

            if (base.ContextoDeTransacao)
                return (await connection.SelectAsync<TEntity>(predicate, base._dbTransaction))?.Any() ?? false;
            else
            {
                using (connection)
                    return (await connection.SelectAsync<TEntity>(predicate))?.Any() ?? false;
            }
        }

        public async Task AlterarExclusao(Guid id)
        {
            Type typeParameterType = typeof(TEntity);
            string tabela = typeParameterType.Name.ToString();
            string sql = $@"UPDATE {tabela} SET Excluido=~Excluido WHERE Id='{id}'";
            await this.QueryAsync(sql, CommandType.Text);
        }

        public async Task AtivaDesativa(object id, string coluna)
        {
            var value = id;
            var keyvalue = $"Id='{id}'";

            if ((value.GetType().FullName == "System.Int32" || value.GetType().FullName == "System.Int64"))
                keyvalue = $"Id={id}";

            Type type = typeof(TEntity);
            string tabela = type.GetTableName();
            string sql = $@"UPDATE [{tabela}] SET {coluna}=~{coluna} WHERE {keyvalue}";
            await this.QueryAsync(sql, CommandType.Text);
        }

        private bool ExistValueKey(TEntity obj)
        {
            var property = typeof(TEntity).GetProperties()
                                          .Where(a => a.IsDefined(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false))
                                          .FirstOrDefault();
            if (property == null)
                throw new Exception("Não foi possível identificar o atributo chave!");

            var value = typeof(TEntity).GetProperty(property.Name).GetValue(obj, null);
            var retorno = true;

            if ((value.GetType().FullName == "System.Int32" || value.GetType().FullName == "System.Int64"))
            {
                retorno = Convert.ToInt64(value) > 0;
            }
            else if ((value.GetType().FullName == "System.Guid"))
            {
                if (((Guid)value) == Guid.Empty)
                {
                    typeof(TEntity).GetProperty(property.Name).SetValue(obj, Guid.NewGuid());
                    retorno = false;
                }
            }

            return retorno;
        }

        private void SetValueKey(TEntity obj, object value)
        {
            var property = typeof(TEntity).GetProperties()
                                          .Where(a => a.IsDefined(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false))
                                          .FirstOrDefault();

            if (property.PropertyType.FullName == "System.Int32")
            {
                typeof(TEntity).GetProperty(property.Name).SetValue(obj, Convert.ToInt32(value));
                return;
            }

            typeof(TEntity).GetProperty(property.Name).SetValue(obj, Convert.ToInt64(value));

        }

        private void SetDate(TEntity entity)
        {
            var property = typeof(TEntity).GetProperties().Where(a => a.Name == "DataCriacao").FirstOrDefault();
            if (property != null)
            {
                var value = typeof(TEntity).GetProperty(property.Name).GetValue(entity, null);
                if (value == null || value.ToString() == DateTime.MinValue.ToString())
                    typeof(TEntity).GetProperty(property.Name).SetValue(entity, DateTime.Now);
            }

            property = typeof(TEntity).GetProperties().Where(a => a.Name == "UltimaAtualizacao").FirstOrDefault();
            if (property != null)
            {
                var value = typeof(TEntity).GetProperty(property.Name).GetValue(entity, null);
                if (value == null || value.ToString() == DateTime.MinValue.ToString())
                    typeof(TEntity).GetProperty(property.Name).SetValue(entity, DateTime.Now);
            }
        }

        public async Task<IEnumerable<TReturn>> MultiMapAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map,
            CommandType commandType = CommandType.Text, object parameters = null, string splitOn = "Id") where T1 : class, TReturn
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.QueryAsync<T1, T2, TReturn>(sql, map, parameters, splitOn: splitOn, commandType: commandType, commandTimeout: 999999, transaction: base._dbTransaction);
            else
            {
                using (connection)
                {
                    return await connection.QueryAsync<T1, T2, TReturn>(sql, map, parameters, splitOn: splitOn, commandType: commandType, commandTimeout: 999999);
                }
            }
        }

        public async Task<IEnumerable<TReturn>> MultiMapAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map,
            CommandType commandType = CommandType.Text, object parameters = null, string splitOn = "Id") where TFirst : class, TReturn
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.QueryAsync<TFirst, TSecond, TThird, TReturn>(sql, map, parameters, this._dbTransaction, splitOn: splitOn, commandType: commandType, commandTimeout: 999999);
            else
            {
                using (connection)
                {
                    return await connection.QueryAsync<TFirst, TSecond, TThird, TReturn>(sql, map, parameters, splitOn: splitOn, commandType: commandType, commandTimeout: 999999);
                }
            }
        }

        public async Task<IEnumerable<TReturn>> MultiMapAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map,
            CommandType commandType = CommandType.Text, object parameters = null, string splitOn = "Id") where TFirst : class, TReturn
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(sql, map, parameters, this._dbTransaction, splitOn: splitOn, commandType: commandType, commandTimeout: 999999);
            else
            {
                using (connection)
                {
                    return await connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(sql, map, parameters, splitOn: splitOn, commandType: commandType, commandTimeout: 999999);
                }
            }
        }

        public async Task<IEnumerable<TReturn>> MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map,
            CommandType commandType = CommandType.Text, object parameters = null, string splitOn = "Id") where TFirst : class, TReturn
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql, map, parameters, this._dbTransaction, splitOn: splitOn, commandType: commandType, commandTimeout: 999999);
            else
            {
                using (connection)
                {
                    return await connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql, map, parameters, splitOn: splitOn, commandType: commandType, commandTimeout: 999999);
                }
            }
        }

        public async Task<IEnumerable<TReturn>> MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map,
            CommandType commandType = CommandType.Text, object parameters = null, string splitOn = "Id") where TFirst : class, TReturn
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(sql, map, parameters, this._dbTransaction, splitOn: splitOn, commandType: commandType, commandTimeout: 999999);
            else
            {
                using (connection)
                {
                    return await connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(sql, map, parameters, splitOn: splitOn, commandType: commandType, commandTimeout: 999999);
                }
            }
        }

        public async Task<IEnumerable<TReturn>> MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map,
            CommandType commandType = CommandType.Text, object parameters = null, string splitOn = "Id") where TFirst : class, TReturn
        {
            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                return await connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(sql, map, parameters, this._dbTransaction, splitOn: splitOn, commandType: commandType, commandTimeout: 999999);
            else
            {
                using (connection)
                {
                    return await connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(sql, map, parameters, splitOn: splitOn, commandType: commandType, commandTimeout: 999999);
                }
            }
        }

        public async Task<SqlMapper.GridReader> ExecuteMultiply(string sql, CommandType commandType = CommandType.Text, object parameters = null)
        {
            SqlMapper.GridReader result = null;

            var connection = base.GetConnection();
            if (base.ContextoDeTransacao)
                result = await connection.QueryMultipleAsync(sql, parameters, this._dbTransaction, commandType: commandType, commandTimeout: 999999);
            else
            {
                result = await connection.QueryMultipleAsync(sql, parameters, commandType: commandType, commandTimeout: 999999);
            }

            return result;
        }
    }
}
