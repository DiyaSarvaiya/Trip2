using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Trip2.BAL
{
    public class CheckAccess : ActionFilterAttribute, IAuthorizationFilter
    {
        // UserID is not there --> redirect to login page
        public void OnAuthorization(AuthorizationFilterContext filtercontext)
        {
            if(filtercontext.HttpContext.Session.GetString("UserID")==null )
            {
                filtercontext.Result = new RedirectResult("~/Users/Login");
            }
        }
        // once logout --> cannot go back to previous page , must login again
        public override void OnResultExecuting(ResultExecutingContext filtercontext)
        {
            filtercontext.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            filtercontext.HttpContext.Response.Headers["Expires"] = "-1";
            filtercontext.HttpContext.Response.Headers["Pragma"] = "no-cache";
            base.OnResultExecuting(filtercontext);
        }
    }
}
