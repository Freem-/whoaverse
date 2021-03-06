﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Whoaverse
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            Application["onlineVisitors"] = 3;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 404)
            {
                Response.Redirect("/error/notfound");
            }
        }

        // fire each time a new session is created     
        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["onlineVisitors"] = (int)Application["onlineVisitors"] + 1;
            Application.UnLock();
        }

        // fire when a session is abandoned or expires
        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["onlineVisitors"] = (int)Application["onlineVisitors"] - 1;
            Application.UnLock();
        }
    }
}
