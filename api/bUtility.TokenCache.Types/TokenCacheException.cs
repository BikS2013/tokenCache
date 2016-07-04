using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.TokenCache.Types
{
    public class TokenCacheException: Exception
    {
        public TokenCacheException(string message): base(message)
        {

        }
        public TokenCacheException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
