using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace TokenCacheHost
{
    public class SecurityTokenResolver : System.IdentityModel.Tokens.X509CertificateStoreTokenResolver
    {
        public SecurityTokenResolver()
            : base("My", StoreLocation.LocalMachine)
        {

        }
        public SecurityTokenResolver(StoreName storeName, StoreLocation storeLocation)
            : base(storeName, storeLocation)
        {
        }
        public SecurityTokenResolver(string storeName, StoreLocation storeLocation)
            : base(storeName, storeLocation)
        {
        }


        protected override bool TryResolveSecurityKeyCore(System.IdentityModel.Tokens.SecurityKeyIdentifierClause keyIdentifierClause, out System.IdentityModel.Tokens.SecurityKey key)
        {
            return base.TryResolveSecurityKeyCore(keyIdentifierClause, out key);
        }
        protected override bool TryResolveTokenCore(System.IdentityModel.Tokens.SecurityKeyIdentifier keyIdentifier, out System.IdentityModel.Tokens.SecurityToken token)
        {
            return base.TryResolveTokenCore(keyIdentifier, out token);
        }
        protected override bool TryResolveTokenCore(System.IdentityModel.Tokens.SecurityKeyIdentifierClause keyIdentifierClause, out System.IdentityModel.Tokens.SecurityToken token)
        {
            return base.TryResolveTokenCore(keyIdentifierClause, out token);
        }
    }
}