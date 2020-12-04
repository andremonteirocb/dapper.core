using System.Data;

namespace Dapper.Core.Interfaces
{
    public interface IRepositoryTransaction
    {
        void SetTransaction(IDbTransaction transation);
    }
}
