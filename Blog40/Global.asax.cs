using AutoMapper;
using Blog40.Models;
using Blog40.Repository;
using Microsoft.Practices.Unity;
using UnityMvc = Microsoft.Practices.Unity.Mvc;
using UnityWebApi = Microsoft.Practices.Unity.WebApi;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Swashbuckle.Application;

namespace Blog40
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private string _databaseConnectionString;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configure(ConfigureWebApi);
            RegisterSwagger(GlobalConfiguration.Configuration);
            ConfigureUnity();
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public void RegisterSwagger(HttpConfiguration config)
        {
            config.EnableSwagger(options =>
                {
                    options.SingleApiVersion("v1", "Blog API");
                }
            ).EnableSwaggerUi();
        }

        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("swagger");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Post",
                url: "{slug}",
                defaults: new { controller = "Home", action = "Post", slug = UrlParameter.Optional }
            );
        }
        private void ConfigureUnity()
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IRepository<Post>>(
                new InjectionFactory(c => new PostRepository(DatabaseConnectionString))    
            );
            container.RegisterType<IRepository<Author>>(
                new InjectionFactory(c => new AuthorRepository(DatabaseConnectionString))
            );
            container.RegisterType<IRepository<Category>>(
                new InjectionFactory(c => new CategoryRepository(DatabaseConnectionString))
            );
            container.RegisterType<IMapper>(
                new InjectionFactory(c => ConfigureAutoMapper())
            );
            GlobalConfiguration.Configuration.DependencyResolver = new UnityWebApi.UnityDependencyResolver(container);
            DependencyResolver.SetResolver(new UnityMvc.UnityDependencyResolver(container));
        }

        private string DatabaseConnectionString
        {
            get
            {
                if (_databaseConnectionString == null)
                {
                    _databaseConnectionString = ConfigurationManager.ConnectionStrings["BlogDb"].ConnectionString;
                }
                return _databaseConnectionString;
            }
        }

        private IMapper ConfigureAutoMapper()
        {
            MapperConfiguration configuration = new MapperConfiguration(config =>
            {
                config.CreateMissingTypeMaps = true;
            });
            return configuration.CreateMapper();
        }
    }
}
