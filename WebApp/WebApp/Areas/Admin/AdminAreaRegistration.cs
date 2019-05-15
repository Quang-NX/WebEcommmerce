using System.Web.Mvc;

namespace WebApp.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "404NotFound",
                "404-NotFound",
                new { action = "NotAccessAuthorize", controller = "Home", id = UrlParameter.Optional },
                namespaces: new[] { "WebApp.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_TrangChu",
                "Admin/Trang-Chu",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional },
                namespaces: new[] { "WebApp.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Login",controller="Home", id = UrlParameter.Optional },
                namespaces: new[] { "WebApp.Areas.Admin.Controllers" }
            );
        }
    }
}