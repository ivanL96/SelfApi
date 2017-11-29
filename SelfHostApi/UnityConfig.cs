using SelfHostApi.Abstract;
using SelfHostApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using Unity;
using Unity.Lifetime;

namespace SelfHostApi
{
    public class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IRepository, MessageRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            //filters
            var providers = config.Services.GetFilterProviders().ToList();

            var defaultprovider = providers.Single(i => i is ActionDescriptorFilterProvider);
            config.Services.Remove(typeof(IFilterProvider), defaultprovider);
            config.Services.Add(typeof(IFilterProvider), new UnityFilterProvider(container));
        }
    }
}