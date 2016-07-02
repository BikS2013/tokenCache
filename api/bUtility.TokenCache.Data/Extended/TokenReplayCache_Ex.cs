using PersistentLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.TokenCache.Data
{
    partial class TokenReplayCache_Ex
    {
        public static string Get(ISqlFactory sqlFactory, string key, out DateTime expirationTime)
        {
            expirationTime = DateTime.Now;

            var token = TokenReplayCache_Ex.FindByKey(sqlFactory, key);
            if (token != null)
            {
                expirationTime = token._ExpirationTime.Value;
                return Encoding.UTF8.GetString(token._SecurityToken);
            }
            return null;
        }

        public static void Remove(ISqlFactory sqlFactory, string key)
        {
            var token = new TokenReplayCache_Ex();
            token.SqlFactory = sqlFactory;
            token._TokenKey = key;
            token.Delete(" TokenKey = @TokenKey ");
        }

        public static bool Contains(ISqlFactory sqlFactory, string key)
        {
            return TokenReplayCache_Ex.FindByKey(sqlFactory, key) != null;
        }

        public static void AddOrUpdate(ISqlFactory sqlFactory, string key, string token, DateTime expirationTime)
        {
            try
            {
                TokenReplayCache_Ex replayRecord = new TokenReplayCache_Ex()
                {
                    _Id = Guid.NewGuid(),
                    _ExpirationTime = expirationTime,
                    _SecurityToken = Encoding.UTF8.GetBytes(token),
                    _TokenKey = key
                };
                replayRecord.SqlFactory = sqlFactory;
                replayRecord.Insert();
            }
            catch (Exception)
            {
                //failed, probably dublicate key, try to update
                try
                {
                    var existingToken = new TokenReplayCache_Ex();
                    existingToken.SqlFactory = sqlFactory;
                    existingToken._TokenKey = key;
                    existingToken._SecurityToken = Encoding.UTF8.GetBytes(token);
                    existingToken._ExpirationTime = expirationTime;
                    existingToken.Update(" SecurityToken = @SecurityToken, ExpirationTime = @ExpirationTime ", " TokenKey = @TokenKey ");
                }
                catch (Exception)
                {
                    //log this
                }
            }
        }

        public static void RemoveAllExpired(ISqlFactory sqlFactory)
        {
            var token = new TokenReplayCache_Ex();
            token.SqlFactory = sqlFactory;
            token._ExpirationTime = DateTime.Now;
            token.Delete(" ExpirationTime < @ExpirationTime ");
        }
    }
}
