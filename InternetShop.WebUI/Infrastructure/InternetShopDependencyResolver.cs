using System;
using System.Collections.Generic;
using System.Web.Mvc;
using InternetShop.DataLayer;
using InternetShop.DataLayer.Abstract;
using Ninject;

namespace InternetShop.WebUI.Infrastructure
{
    public class InternetShopDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public InternetShopDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IProductsRepository>().To<ProductsRepository>();
        }
    }
}