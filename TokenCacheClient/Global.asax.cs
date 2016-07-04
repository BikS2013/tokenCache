using System;
using System.Web;
using System.Net;
using System.IdentityModel.Services;
using System.Diagnostics;
using bUtility.Logging;

namespace TokenCacheClient
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Logger.SetCurrent( new Logger("TokenCacheSource"));


            // Remove Header X-AspNetMvc-Version
            //MvcHandler.DisableMvcResponseHeader = true;
            //AreaRegistration.RegisterAllAreas();
            var cp = ConfigProfile.LoadConfigurationProfile();
            WebApiConfig.Configure(cp);
        }

        protected void Application_BeginRequest(Object source, EventArgs e)
        {
            //  This corrects WIF error ID3206 "A SignInResponse message may only redirect within the current web application: '/NHP' is not allowed."
            //  For whatever reason, accessing the site without a trailing slash causes this error.
            if (String.Compare(Request.Path, Request.ApplicationPath, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                !(Request.Path.EndsWith("/")))
            {
                Response.Redirect(Request.Path + "/", false);
                // Avoid thread abort exception
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();    // Log the exception.     
            Response.Clear();
            // Clear the error on server.   
            Server.ClearError();    // Avoid IIS7 getting in the middle  
            if (exception is System.OperationCanceledException)
            {
                Logger.Current.Info(exception);
            }
            else
            {
                Logger.Current.Error(exception);
            }
            //CRMUtility.XmlExceptionPublisher.Publish(EventLogEntryType.Error, exception, "Web application error");
            Response.TrySkipIisCustomErrors = true;

            try
            {
                if (exception is System.IdentityModel.Tokens.SecurityTokenException ||
                    exception is System.Security.SecurityException)
                {
                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    Response.StatusDescription = "Error";
                }
            }
            catch (System.Web.HttpException)
            {
                // Too late, headers have been sent already.
            }
        }

        protected void WSFederationAuthenticationModule_AuthorizationFailed(object sender, AuthorizationFailedEventArgs e)
        {
            //if (SessionModule.ShouldReturn401OnAuthError(HttpContext.Current.Request))
            //    e.RedirectToIdentityProvider = false;
        }

        protected void WSFederationAuthenticationModule_RedirectingToIdentityProvider(object sender, RedirectingToIdentityProviderEventArgs e)
        {
            //if (SessionModule.ShouldReturn401OnAuthError(HttpContext.Current.Request))
            //    e.Cancel = true;
        }

        protected void Session_Start(Object source, EventArgs e)
        {
            var session = HttpContext.Current.Session;
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
        }

        //γυρίζει το WSFederation σε Reference mode
        void WSFederationAuthenticationModule_SessionSecurityTokenCreated
          (object sender, SessionSecurityTokenCreatedEventArgs e)
        {
            e.SessionToken.IsReferenceMode = true;
        }

    }
}
