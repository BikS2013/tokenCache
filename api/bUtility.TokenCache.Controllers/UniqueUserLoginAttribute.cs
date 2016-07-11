using System;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
using bUtility.TokenCache.Implementation;
using bUtility.TokenCache.Interfaces;
using bUtility.Logging;

namespace bUtility.TokenCache.Filters
{
    public class UniqueUserLoginAttribute : ActionFilterAttribute
    {
        private readonly Func<PersistentLib.ISqlFactory> _sqlConnector;
        private readonly int _rollingExpiryWindowInMinutes; // = 65
        public UniqueUserLoginAttribute(Func<PersistentLib.ISqlFactory> sqlConnector, int rollingExpiryWindowInMinutes)
        {
            _sqlConnector = sqlConnector;
            _rollingExpiryWindowInMinutes = rollingExpiryWindowInMinutes;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            bool failed = false;
            string tokenID = null;
            string userName = "";

            try
            {
                var identity = Thread.CurrentPrincipal?.Identity;
                if (identity is ClaimsIdentity && identity.IsAuthenticated && identity.Name != Constants.SystemIdentity)
                {
                    userName = Thread.CurrentPrincipal.Identity.Name;
                    var claimsIdentity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
                    if (claimsIdentity != null && claimsIdentity.BootstrapContext != null)
                    {
                        var bootstrap = claimsIdentity.BootstrapContext as BootstrapContext;
                        if (bootstrap != null && bootstrap.SecurityToken != null)
                        { 
                            tokenID = bootstrap.SecurityToken.Id;
                        }
                    }
                    //we check the db if there is a compination of of the user and tokenID AND the token is not expired by the rolling timeout window
                    //if yes, we will update the rolling session timeout by the rollingSessionTimeoutPeriod
                    //there is no way that we will get same id & userName for different endpointIDs and we get here. Think about it
                    if (!string.IsNullOrEmpty(tokenID) && 
                        !string.IsNullOrEmpty(userName) &&
                        !DistributedSessionSecurityTokenCache.CheckForUserTokenAndUpdateRollingTimeout(_sqlConnector, userName, tokenID, _rollingExpiryWindowInMinutes))
                        failed = true;
                }
            }
            catch (Exception ex)
            {
                failed = true;
                Logger.Current.Error(ex);
            }
            if (failed)
            {
                Logger.Current.Error($"Duplicate login or Rolling Session Expired: {Thread.CurrentPrincipal?.Identity?.Name}");
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Error 5.1.1.TC");
            }
            else
                base.OnActionExecuting(actionContext);
        }
    }
}