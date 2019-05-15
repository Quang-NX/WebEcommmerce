using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebApp.Mapping;

namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapper.Mapper.Initialize(m => m.AddProfile<MappingProfile>());
            //init count view = 0
            Application["PageView"] = 0;
            Application["Online"] = 0;
        }
        //Một pageView sẽ được tính khi có một người vào website của chúng ta khi đó phương thức session_start đã đưuọc chạy
        protected void Session_Start()
        {
            Application.Lock();//dùng để đồng bộ hóa tuần tự.tránh trường hợp cùng lúc có quá nhiều user truy cập.
            Application["PageView"] = (int)Application["PageView"] + 1;
            Application["Online"] = (int)Application["Online"] + 1;
            Application.UnLock(); 
        }
        //giảm đi nếu ngừng truy cập
        protected void Session_End()
        {
            Application.Lock();//dùng để đồng bộ hóa tuần tự.tránh trường hợp cùng lúc có quá nhiều user truy cập.
            Application["Online"] = (int)Application["Online"] - 1;
            Application.UnLock();
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var TaiKhoanCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (TaiKhoanCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(TaiKhoanCookie.Value);//khi nhận đk request từ client thì server sẽ giải mã cookie đó
                //var Quyen = authTicket.UserData.Split(new Char[] { ',' });
                var Quyen = authTicket.UserData.Split(new Char[] { ' ' });
                var userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), Quyen);
                Context.User = userPrincipal;
            }

        }
    }
}
