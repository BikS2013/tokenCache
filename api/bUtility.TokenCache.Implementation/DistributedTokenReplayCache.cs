using bUtility.TokenCache.Types.Replay;
using bUtility.TokenCache.Data;
using PersistentLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bUtility.TokenCache.Interfaces;

namespace bUtility.TokenCache.Implementation
{
    public class DistributedTokenReplayCache : IDistributedTokenReplayCache
    {
        private readonly Func<PersistentLib.ISqlFactory> _sqlConnector;
        public DistributedTokenReplayCache(Func<PersistentLib.ISqlFactory> sqlConnector)
        {
            _sqlConnector = sqlConnector;
        }
        private ISqlFactory GetSqlServerFactory()
        {
            return _sqlConnector();
        }

        public bool AddOrUpdate(ReplayCacheEntry request)
        {
            using (var sqlFactory = GetSqlServerFactory())
            {
                TokenReplayCache_Ex.AddOrUpdate(
                         sqlFactory,
                         request.Key,
                         request.SecurityToken,
                         request.ExpirationTime.ToLocalTime());

                return true;
            }
        }


        public bool Contains(string key)
        {
            using (var sqlFactory = GetSqlServerFactory())
            {
                var response = TokenReplayCache_Ex.Contains(
                    sqlFactory,
                    key);

                return response;
            }
        }

        public ReplayCacheEntry Get(string key)
        {
            using (var sqlFactory = GetSqlServerFactory())
            {
                var response = new ReplayCacheEntry();

                DateTime expirationTime = DateTime.Now;
                var token = TokenReplayCache_Ex.Get(
                    sqlFactory,
                    key,
                    out expirationTime);

                if (token != null)
                {
                    response.SecurityToken = token;
                    response.ExpirationTime = expirationTime;
                    response.Key = key;

                }
                return response;
            }
        }

        public bool Remove(string key)
        {
            using (var sqlFactory = GetSqlServerFactory())
            {
                TokenReplayCache_Ex.Remove(
                    sqlFactory,
                    key);

                return true;
            }
        }

        public bool Purge()
        {
            using (var sqlFactory = GetSqlServerFactory())
            {
                TokenReplayCache_Ex.RemoveAllExpired(
                    sqlFactory);

                return true;
            }
        }
    }
}
