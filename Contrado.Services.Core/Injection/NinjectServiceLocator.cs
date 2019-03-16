using Microsoft.Practices.ServiceLocation;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Services.Core.Injection
{
    /// <summary>
    /// The current state of CommonServiceLocator implementations that are
    /// available via NuGet is poor, in that they introduce incompatabilities
    /// between the standard framework and the extension. Since this is a
    /// simple service, an implementation is provided here until such time
    /// as this situation is fixed.
    /// NB: Service location is provided ONLY for use by clients that are
    /// outside of the DI scope - attributes, handlers, modules, etc. - and
    /// that are created prior to the container being spun up. All other
    /// items should have no use for this service locators. Remember that
    /// ServiceLocator is an anti-pattern.
    /// </summary>
    public class NinjectServiceLocator : ServiceLocatorImplBase
    {
        /// <summary>
        /// Creates a new NinjectServiceLocator that consults the given kernel.
        /// </summary>
        /// <param name="kernel">the kernel that represents the service to be located</param>
        public NinjectServiceLocator(IKernel kernel)
        {
            this.Kernel = kernel;
        }

        /// <summary>
        /// The kernel that is being accessed via this service locator.
        /// </summary>
        public IKernel Kernel { get; private set; }

        /// <summary>
        /// Performs a lookup of the service of the given type, returning the
        /// first binding that has the name (key) provided. This key may be null.
        /// </summary>
        /// <param name="serviceType">the type of service to return</param>
        /// <param name="key">an optional key to restrict by</param>
        /// <returns>the first service found for these parameters</returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            return key == null ? Kernel.Get(serviceType) : Kernel.Get(serviceType, key);
        }

        /// <summary>
        /// Returns all registered instances of the specified service type.
        /// </summary>
        /// <param name="serviceType">the type of service to be returned</param>
        /// <returns>all instances currently bound to this service type</returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }
    }
}
