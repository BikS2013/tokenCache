using PersistentLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.TokenCache.Data
{
    partial class SecurityTokenCache_Ex
    {
        public static bool AddOrUpdate(ISqlFactory sqlFactory,
            string endpointID,
            string contextID,
            string keyGeneration,
            string securityTokenValue,
            string sessionSecurityTokenID,
            System.DateTime expiryTime,
            System.DateTime rollingExpiryTime,
            string userName)
        {
            if (securityTokenValue == null || sessionSecurityTokenID == null)
                return false;

            try
            {
                SecurityTokenCache_Ex token = new SecurityTokenCache_Ex()
                {
                    _Id = Guid.NewGuid(),
                    _ContextID = contextID,
                    _EndpointID = endpointID,
                    _KeyGeneration = keyGeneration,
                    _ExpiryTime = expiryTime,
                    _SessionSecurityTokenValue = Encoding.UTF8.GetBytes(securityTokenValue),
                    _SessionSecurityTokenID = sessionSecurityTokenID,
                    _UserName = userName,
                    _RollingExpiryTime = rollingExpiryTime,
                    SqlFactory = sqlFactory
                };
                token.Insert();
            }
            catch (Exception)
            {
                //failed, probably dublicate key, try to update
                try
                {
                    var existingToken = new SecurityTokenCache_Ex()
                    {
                        _ContextID = contextID,
                        _EndpointID = endpointID,
                        _KeyGeneration = keyGeneration,
                        _ExpiryTime = expiryTime,
                        _SessionSecurityTokenValue = Encoding.UTF8.GetBytes(securityTokenValue),
                        _SessionSecurityTokenID = sessionSecurityTokenID,
                        _UserName = userName,
                        _RollingExpiryTime = rollingExpiryTime,
                        SqlFactory = sqlFactory
                    };
                    int updated = existingToken.Update("ContextID = @ContextID, EndpointID = @EndpointID, KeyGeneration = @KeyGeneration, ExpiryTime = @ExpiryTime, RollingExpiryTime = @RollingExpiryTime, SessionSecurityTokenValue = @SessionSecurityTokenValue, SessionSecurityTokenID = @SessionSecurityTokenID ", " UserName = @UserName and EndpointID = @EndpointID and ExpiryTime <= @ExpiryTime");
                    if (updated == 0)
                        throw new Exception("Newer token in cache.");
                }
                catch (Exception ex)
                {
                    new Exception("UserName: " + userName,  ex).ToEventLog();
                    return false;
                }
            }
            return true;
        }
        public static IEnumerable<string> GetAll(ISqlFactory sqlFactory, string endpointID, string contextID)
        {
            var result = new List<string>();
            var res = SecurityTokenCache_Ex.FindAllByEndpointIDAndContextID(sqlFactory, endpointID, contextID);
            foreach (var item in res)
            {
                result.Add(Encoding.UTF8.GetString(item._SessionSecurityTokenValue));
            }
            return result;
        }

        public static string Get(ISqlFactory sqlFactory, string endpointID, string contextID, string keyGeneration)
        {
            SecurityTokenCache_Ex res = null;
            if (keyGeneration != null)
            {
                res = SecurityTokenCache_Ex.FindByEndpointIDContextIDAndKeyGeneration(sqlFactory, endpointID, contextID, keyGeneration);
            }
            else
            {
                res = SecurityTokenCache_Ex.FindByEndpointIDAndContextID(sqlFactory, endpointID, contextID);

            }
            if (res != null && res._SessionSecurityTokenValue != null)
                return Encoding.UTF8.GetString(res._SessionSecurityTokenValue);
            return null;
        }

        public static void Remove(ISqlFactory sqlFactory, string endpointID, string contextID, string keyGeneration)
        {
            var token = new SecurityTokenCache_Ex();
            token.SqlFactory = sqlFactory;
            token._EndpointID = endpointID;
            token._ContextID = contextID;
            if (keyGeneration != null)
            {
                token._KeyGeneration = keyGeneration;
                token.Delete(" EndpointID = @EndpointID and ContextID = @ContextID and KeyGeneration = @KeyGeneration");
            }
            else
            {
                token.Delete(" EndpointID = @EndpointID and ContextID = @ContextID ");
            }
        }

        public static void RemoveAll(ISqlFactory sqlFactory, string p)
        {
            throw new System.NotImplementedException();
        }

        public static void RemoveAll(ISqlFactory sqlFactory, string endpointID, string contextID)
        {
            var token = new SecurityTokenCache_Ex();
            token.SqlFactory = sqlFactory;
            token._EndpointID = endpointID;
            token._ContextID = contextID;
            token.Delete(" EndpointID = @EndpointID and _ContextID = @ContextID ");
        }

        public static bool CheckForUserTokenAndUpdateRollingTimeout(ISqlFactory sqlFactory,
            string user,
            string id,
            System.DateTime rollingExpiryTime,
            System.DateTime currentTime)
        {
            bool successResult = false;
            try
            {
                SecurityTokenCache_Ex item = new SecurityTokenCache_Ex();
                item.SqlFactory = sqlFactory;
                item._UserName = user;
                item._SessionSecurityTokenID = id;
                item._RollingExpiryTime = rollingExpiryTime;
                item._CurrentTime = currentTime;

                int result = item.Update(" RollingExpiryTime = @RollingExpiryTime", " UserName = @UserName and SessionSecurityTokenID = @SessionSecurityTokenID and RollingExpiryTime > @CurrentTime");
                if (result > 0)
                    successResult = true;                
            }
            catch (Exception ex)
            {
                new Exception("UserName: " + user, ex).ToEventLog();
                return false; 
            }
            return successResult;
        }

    }
}
