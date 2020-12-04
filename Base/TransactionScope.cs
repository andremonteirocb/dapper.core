using Dapper.Core.Interfaces;
using System;
using System.Data;

namespace Dapper.Core.Base
{
    public class TransactionScope : IDisposable
    {
        private IRepositoryTransaction[] _repositories;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public TransactionScope()
        {
            this.OpenConnection();
            
        }
        public TransactionScope(params IRepositoryTransaction[] repositories) : this()
        {
            _repositories = repositories;
            this.SetTranction(_transaction);
        }

        private void OpenConnection()
        {
            DataBaseContext context = new DataBaseContext();
            this._connection = context.GetConnection();
            this._transaction = _connection.BeginTransaction();
        }

        public void Complete()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }

            this.SetTranction();
        }

        public IDbTransaction GetTransaction() => _transaction;

        private void SetTranction(IDbTransaction transaction = null)
        {
            foreach (var repository in _repositories)
                repository.SetTransaction(transaction);
        }
    }
}
