using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Diagnostics;
using System.Net.Http.Formatting;
using bUtility.Logging;

namespace TokenCacheClient
{
    public static class WebApiConfig
    {
        public static void Configure(ConfigProfile cp)
        {
            try
            {
                GlobalConfiguration.Configure((httpConf) =>
                {
                    RegisterRoutes(httpConf);
                    //RegisterGlobalFilters(httpConf.Filters, cp);
                    //CustomizeFormatters(httpConf.Formatters);
                    //RegisterHandlers(httpConf.MessageHandlers, cp);
                    RegisterLocalServices(cp);
                });
            }
            catch(Exception ex)
            {
                Logger.Current.Error(ex);
            }

        }
        private static void RegisterLocalServices(ConfigProfile cp)
        {
            try
            {
                throw new ApplicationException("test Exception");
            }
            catch (Exception ex)
            {
                Logger.Current.Error(ex);
            }
            try
            {
#warning το θέλουμε ή όχι;
                //filters.Add(new AuditFilterAttribute());

                Func<PersistentLib.ISqlFactory> ibSqlFactoryProvider = () => { return new PersistentLib.SqlServerFactory(cp.TokenCacheConnection); };

            }
            catch (Exception ex)
            {
                Logger.Current.Error(ex);
                throw;
            }
        }

        public static readonly string CONTROLLER_ACTION = "ControllerAction";
        public static readonly string PRIVATE_CONTROLLER_ACTION = "PrivateControllerAction";
        public static void RegisterRoutes(HttpConfiguration config)
        {
            //κάνει enable το attribute routing
            config.MapHttpAttributeRoutes();

            // CSRF tokens in GET requests are potentially leaked at several locations: 
            // browser history, HTTP log files, network appliances that make a point to 
            // log the first line of an HTTP request, and Referrer headers if the protected 
            // site links to an external site. This is why we don't add the /{token} in route.
            config.Routes.MapHttpRoute(
                name: CONTROLLER_ACTION,
                routeTemplate: "{controller}/{action}",
                defaults: null//new { token = ""}
            );

            config.Routes.MapHttpRoute(
                name: PRIVATE_CONTROLLER_ACTION,
                routeTemplate: "privateApi/{controller}/{action}",
                defaults: null
            );
        }

        public static void RegisterGlobalFilters(HttpFilterCollection filters, ConfigProfile cp)
        {
            //filters.Add(new ExceptionHandlingAttribute());
            //filters.Add(new NoCacheAttribute());
            //if (cp.RequireHttps)
            //{
            //    // Require https only connection
            //    filters.Add(new RequireHttpsAttribute());
            //}
            //else
            //{
            //    Logger.Current.Warn("Require HTTPS is disabled for web api");
            //}
            // Require a valid caller Identity
            //filters.Add(new ClaimsAuthorizeAttribute());


            //filters.Add(new RequestValidUserIDAttribute());
            Func<PersistentLib.ISqlFactory> auditSqlFactoryProvider = () => { return new PersistentLib.SqlServerFactory(cp.InternetBankingAuditConnection); };
            if (cp.EnforceUniqueUserLogin)
                filters.Add(new bUtility.TokenCache.Filters.UniqueUserLoginAttribute(
                    auditSqlFactoryProvider,
                    cp.RollingExpiryWindowInMinutes));

            // Depricated, using httpHandler for xsrf instead
            //filters.Add(new WebApiValidateAntiForgeryTokenAttribute())
        }

        public static void CustomizeFormatters(MediaTypeFormatterCollection formatters)
        {
            formatters.Remove(formatters.XmlFormatter);

            SetJsonSerializationSettings(formatters.JsonFormatter.SerializerSettings);
        }

        public static Newtonsoft.Json.JsonSerializerSettings GetSerializationSettings()
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
            SetJsonSerializationSettings(settings);
            return settings;
        }

        private static void SetJsonSerializationSettings(Newtonsoft.Json.JsonSerializerSettings settings)
        {
            //settings.ContractResolver = new SensitiveDataContractResolver();
            settings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            //settings.Error = ExceptionHandlingAttribute.OnSerializationError;
        }
        private static void RegisterHandlers(
            System.Collections.ObjectModel.Collection<System.Net.Http.DelegatingHandler> handlers,
            ConfigProfile cp)
        {
            //if (cp.MessageLoggingEnabled) handlers.Add(new MessageLoggingHandler("WebApi"));

            //handlers.Add(new bUtility.Handlers.AuthenticationHandler());

            //handlers.Add(new bUtility.Handlers.LanguageMessageHandler());


            //handlers.Add(new bUtility.Handlers.ThrottlingHandler(999, 0));
        }
    }
}
