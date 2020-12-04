using Microsoft.Extensions.Configuration;

namespace Dapper.Core.Configurations
{
    public static class ConfiguracaoRepository
    {
        public static string ConnectionString;
        public static void Configure(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}