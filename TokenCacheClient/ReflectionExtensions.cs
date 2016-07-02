using bUtility.TokenCache.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace TokenCacheClient
{
    public static class ReflectionExtensions
    {
        public static MethodInfo GetAction(this Type controllerType, string actionName)
        {
            if (controllerType != null)
            {
                var methods = controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                if (methods != null)
                {
                    foreach (var m in methods)
                    {
                        var attr = m.GetCustomAttribute(typeof(ActionNameAttribute));
                        if (attr != null && (attr as ActionNameAttribute).Name == actionName)
                        {
                            return m;
                        }
                    }
                }
            }
            return null;
        }

        public static object GetInstance(this Type controllerType)
        {
            var constructor = controllerType.GetConstructor(System.Type.EmptyTypes);
            if (constructor != null)
            {
                var instance = constructor.Invoke(null);
                return instance;
            }
            return null;
        }

        public static void SetProperty(this object obj, string propertyName, object value)
        {
            if (obj != null)
            {
                try
                {
                    var pinfo = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                    if (pinfo != null)
                    {
                        pinfo.SetValue(obj, value);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Type: " + obj.GetType().Name + " PropertyName: " + propertyName, ex);
                }
            }
        }

        public static object GetPayload(this MethodInfo action, string jsonPayload)
        {
            var requestType = action.GetParameters()[0].ParameterType;
            var payloadType = requestType.GetGenericArguments()[0];

            var settings = WebApiConfig.GetSerializationSettings();
            //settings.ContractResolver = new AuditingContractResolver();
            var payload = JsonConvert.DeserializeObject(jsonPayload, payloadType, settings);
            return payload;
        }

        public static object GetRequest(this MethodInfo action, RequestHeader header, object payload)
        {
            var requestType = action.GetParameters()[0].ParameterType;
            var request = requestType.GetConstructor(System.Type.EmptyTypes).Invoke(null);
            request.SetProperty("Header", header);
            request.SetProperty("Payload", payload);
            return request;
        }
    }
}