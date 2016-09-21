using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IC.WebApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //IC.Framework.Constants.ICConstant.APNSCertificatePathForDev = HttpContext.Current.Server.MapPath("Content\\PushDevKey.p12");
            IC.Framework.Constants.ICConstant.APNSCertificatePathForProd = HttpContext.Current.Server.MapPath("Content\\Certificates_Dis.p12");
        }
    }
}
