using Newtonsoft.Json;

namespace TokenCacheClient
{
    public static class JsonTranslator
    {
        public static string ToJson(object value)
        {
            return JsonConvert.SerializeObject(value, WebApiConfig.GetSerializationSettings());
        }

        //public static string ToAuditable(object value)
        //{
        //    var settings = WebApiConfig.GetSerializationSettings();
        //    settings.ContractResolver = new AuditingContractResolver();
        //    return JsonConvert.SerializeObject(value, settings);
        //}
    }
}