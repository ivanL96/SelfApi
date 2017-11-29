using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using Unity;
using Unity.Exceptions;

namespace SelfHostApi
{
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            container.Dispose();
        }
    }

    // do IOC for filters.
    public class UnityFilterProvider : IFilterProvider
    {
        private IUnityContainer container;
        private readonly ActionDescriptorFilterProvider defaultProvider = new ActionDescriptorFilterProvider();

        public UnityFilterProvider(IUnityContainer container)
        {
            this.container = container;
        }

        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration,
            HttpActionDescriptor actionDescriptor)
        {
            var attributes = defaultProvider.GetFilters(configuration, actionDescriptor);

            foreach (var attr in attributes)
            {
                container.BuildUp(attr.Instance.GetType(), attr.Instance);
            }
            return attributes;
        }
    }
}