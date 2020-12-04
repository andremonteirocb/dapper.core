using Dapper.Core.Configurations;
using System.Data;
using System.Data.SqlClient;

namespace Dapper.Core.Base
{
    public class DataBaseContext
    {
        protected IDbTransaction _dbTransaction;
        protected bool ContextoDeTransacao => _dbTransaction != null;

        public IDbConnection GetConnection()
        {
            if (_dbTransaction != null)
                return (IDbConnection)_dbTransaction?.Connection;

            var connection = new SqlConnection(ConfiguracaoRepository.ConnectionString);
            connection.Open();
            return (IDbConnection)connection;
        }
        public void SetTransaction(IDbTransaction transaction)
        {
            _dbTransaction = transaction;
        }
    }
}
