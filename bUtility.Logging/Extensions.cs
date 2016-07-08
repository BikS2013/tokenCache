using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.Logging
{
    public static class Extensions
    {
        public static string ToJSON(this Exception ex)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(ex, new Newtonsoft.Json.JsonSerializerSettings { Formatting = Newtonsoft.Json.Formatting.Indented });
        }

    }
}
