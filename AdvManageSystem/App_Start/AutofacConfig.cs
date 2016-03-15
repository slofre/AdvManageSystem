using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Model.Repositories;
using Model.DataInterfaces;

namespace AdvManageSystem.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {           
            var builder = new ContainerBuilder();
                        
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
                       
            builder.RegisterType<AdvertisementRepository>().As<IAdvertisementRepository>();
           
            var container = builder.Build();
         
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}