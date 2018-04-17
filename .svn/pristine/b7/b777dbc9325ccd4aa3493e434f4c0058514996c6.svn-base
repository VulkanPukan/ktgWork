using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StructureMap;
using StrengthTracker2.DependencyResolution.DependencyResolution;
using StrengthTracker2.Web.Helpers;

namespace StrengthTracker2.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			//ModelBinders.Binders.Add(typeof(bool), new BooleanModelBinder());
			IoC.Initialize();
        }

        protected void Session_Start()
        {
        }

		protected void Application_Error(object sender, EventArgs e)
		{
		var error = Server.GetLastError();
			Server.ClearError();
			Response.ContentType = "text/plain";
			Response.Write(error ?? (object)"unknown");
			Response.End();
		}
	}
}