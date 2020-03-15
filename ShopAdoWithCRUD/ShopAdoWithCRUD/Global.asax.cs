using ShopAdoWithCRUD.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopAdoWithCRUD
{
    public class MvcApplication : System.Web.HttpApplication
    {
        GlobalFilterCollection filter = new GlobalFilterCollection();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(new MyDependencyResolver());

            filter.Add(new HandleErrorAttribute());
        }
    }
}
