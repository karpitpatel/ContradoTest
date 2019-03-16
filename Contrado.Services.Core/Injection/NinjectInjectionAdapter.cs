using Contrado.Core.Injection;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Services.Core.Injection
{
    /// <summary>
    /// An implementation of IInjectionAdapter for Ninject, which translates
    /// all of the generic members of IInjectionAdapter into their Ninject-based
    /// equivalent. The adapter works on a Ninject kernel, which is supplied
    /// during construction.
    /// </summary>
    public class NinjectInjectionAdapter : IInjectionAdapter
    {
        private readonly IKernel kernel;

        public NinjectInjectionAdapter(IKernel kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// Creates a binding between the given services, such that requests for
        /// service T1 are resolved to instances of T2. This binding exists
        /// within a transient scope.
        /// </summary>
        /// <typeparam name="T1">the first service to be bound</typeparam>
        /// <typeparam name="T2">the second service to be bound</typeparam>
        /// <returns>this adapter, for use in chaining requests</returns>
        public IInjectionAdapter Bind<T1, T2>() where T2 : T1
        {
            kernel.Bind<T1>().To<T2>();

            return this;
        }

        /// <summary>
        /// Creates a binding between the given services in the scope specified,
        /// such that requests for service T1 are resolved to instances of T2.
        /// The specified scope is translated into its equivalent Ninject scope.
        /// </summary>
        /// <typeparam name="T1">the first service to be bound</typeparam>
        /// <typeparam name="T2">the second service to be bound</typeparam>
        /// <param name="scope">the scope to apply to this binding</param>
        /// <returns>this adapter, for use in chaining requests</returns>
        public IInjectionAdapter Bind<T1, T2>(InjectionScope scope) where T2 : T1
        {
            var binding = kernel.Bind<T1>().To<T2>();

            switch (scope)
            {
                case InjectionScope.Singleton:
                    binding.InSingletonScope();
                    break;

                case InjectionScope.Thread:
                    binding.InThreadScope();
                    break;

                case InjectionScope.Request:
                    binding.InRequestScope();
                    break;

                default:
                    binding.InTransientScope();
                    break;
            }

            return this;
        }

        /// <summary>
        /// Binds the first service type to the second, with an optional set of
        /// constructor arguments with which to instantiate the target when invoked.
        /// </summary>
        /// <typeparam name="T1">the first service to be bound</typeparam>
        /// <typeparam name="T2">the second service to be bound</typeparam>
        /// <param name="constructorArguments">constructor arguments for the latter</param>
        /// <returns>a reference back to this object, for chaining</returns>
        public IInjectionAdapter Bind<T1, T2>(IDictionary<string, object> constructorArguments) where T2 : T1
        {
            var binding = kernel.Bind<T1>().To<T2>();

            if (constructorArguments != null && constructorArguments.Keys.Count > 0)
            {
                var keyList = new List<string>(constructorArguments.Keys);
                var bindingWithSyntax = binding.WithConstructorArgument(keyList[0], constructorArguments[keyList[0]]);

                for (var i = 1; i < keyList.Count; i++)
                {
                    bindingWithSyntax = bindingWithSyntax.WithConstructorArgument(keyList[i], constructorArguments[keyList[i]]);
                }
            }

            return this;
        }

        /// <summary>
        /// Removes the binding for the given service.
        /// </summary>
        /// <typeparam name="T1">the service whose binding is to be removed</typeparam>
        /// <returns>this adapter, for use in chaining requests</returns>
        public IInjectionAdapter Unbind<T1>()
        {
            kernel.Unbind<T1>();
            return this;
        }

        /// <summary>
        /// Attempts to return a binding for the given service. If no such binding
        /// can be found, null is returned.
        /// </summary>
        /// <typeparam name="T1">the service to search for</typeparam>
        /// <returns>the service currently bound to sevice T1; or null</returns>
        public T1 TryGet<T1>()
        {
            return kernel.TryGet<T1>();
        }

        /// <summary>
        /// Creates a binding between the given services, such that requests for
        /// service T1 are resolved to instances of T2 with contextual binding.
        /// This binding exists within a transient scope.
        /// Use this method if single interface implemented by multiple classes.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="contextualBindingParameter">The contextual binding parameter.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IInjectionAdapter Bind<T1, T2>(string contextualBindingParameter) where T2 : T1
        {
            kernel.Bind<T1>().To<T2>().Named(contextualBindingParameter);

            return this;
        }
    }
}
