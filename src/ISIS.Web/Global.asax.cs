﻿using System.Web.Mvc;
using System.Web.Routing;
using Ncqrs.Config.Ninject;
using Ninject;

namespace ISIS.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ConfigureInjection();
        }

        protected void ConfigureInjection()
        {
            var kernel = new StandardKernel(
                new KernelModule(),
                new ValidatorModule(),
                new SetValidatorModule(),
                new NcqrsModule(),
                new NHibernateModule());

            Ncqrs.NcqrsEnvironment.Configure(new NinjectConfiguration(kernel));
            var controllerFactory = new NinjectControllerFactory(kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }


    }
}