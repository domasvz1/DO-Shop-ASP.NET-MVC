using System;
using System.Web.Mvc;

namespace Presentation
{
    public class UserAuthorization : AuthorizeAttribute
    {
        public string ConnectionPage { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.IsInRole(Roles))
            {
                var returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped);
                filterContext.HttpContext.Response.Redirect(ConnectionPage + "?ReturnUrl=" + returnUrl);
            }
            base.OnAuthorization(filterContext);
        }
    }
}