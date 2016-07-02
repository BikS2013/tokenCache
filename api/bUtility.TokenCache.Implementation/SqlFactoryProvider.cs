using PersistentLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.TokenCache.Implementation
{
    public interface ISqlFactoryProvider
    {
        ISqlFactory GetSqlFactory();
    }
    public class SqlFactoryProvider : ISqlFactoryProvider
    {
        readonly string _connectionString;
        public SqlFactoryProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ISqlFactory GetSqlFactory()
        {
            return new SqlServerFactory(_connectionString);
        }
    }
}
