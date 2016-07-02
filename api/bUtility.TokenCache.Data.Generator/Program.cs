using PersistentLib;
using PersistentLib.Extender;
using PersistentLib.Generator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.TokenCache.Data.Generator
{
    internal class Program
    {
        private const string Path = @"..\..\..\bUtility.Data.IdentityModel\";
        private const string BasePath = Path + @"Base";
        private const string ExtendedPath = Path + @"Extended";
        private const string NameSpace = "bUtility.Data.IdentityModel";

        private static Table GetTableSimple(Model m, string tableName)
        {
            var t = new Table(tableName, tableName + "_Ex", tableName);
            m.Add(t);
            t.Add(new TableMethod("FindAll", MethodType.list));
            return t;
        }

        private static void Main()
        {
            using (var sf = new SqlServerFactory(ConfigurationManager.AppSettings["tokenCacheConnection"]))
            {
                var gf = new GeneratorFactory(sf);

                gf.BuildTable("SecurityTokenCache", NameSpace, BasePath);

                var m = new Model(gf, NameSpace);

                var securityTokenCache = GetTableSimple(m, "SecurityTokenCache");

                securityTokenCache.PrimaryKey = new PrimaryKey("pk", "_ContextID", "_EndpointID");
                securityTokenCache.Add(new TableMethod("FindAllByEndpointIDAndContextID", MethodType.list, new[] { "_EndpointID", "_ContextID" }));
                securityTokenCache.Add(new TableMethod("FindByEndpointIDContextIDAndKeyGeneration", MethodType.find, new[] { "_EndpointID", "_ContextID", "_KeyGeneration" }));
                securityTokenCache.Add(new TableMethod("FindByEndpointIDAndContextID", MethodType.find, new[] { "_EndpointID", "_ContextID" }));

                securityTokenCache.Parameters.Add(new Parameter("CurrentTime", typeof(System.DateTime)));

                gf.BuildTable("TokenReplayCache", NameSpace, BasePath);

                var tokenReplayCache = GetTableSimple(m, "TokenReplayCache");

                tokenReplayCache.PrimaryKey = new PrimaryKey("pk", "_TokenKey");
                tokenReplayCache.Add(new TableMethod("FindByKey", MethodType.find, new[] { "_TokenKey" }));

                m.BuildCode(gf, ExtendedPath);
            }
        }
    }
}
