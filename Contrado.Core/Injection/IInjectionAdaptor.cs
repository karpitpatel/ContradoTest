using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Core.Injection
{
    /// <summary>
    /// Captures a generic set of injection scopes for supported containers.
    /// This set is subject to change if/as the supported container/scopes change.
    /// </summary>
    public enum InjectionScope
    {
        Default,
        Singleton,
        Thread,
        Request,
        Parent
    }

    /// <summary>
    /// Wraps a particular IoC container implementation, providing a generic
    /// form of the the contract that it defines. Use of this adapter limits
    /// the specifics of a particular injection engine to the host (web, console,
    /// etc.) application, without forcing this dependency on the entire codebase.
    /// </summary>
    public interface IInjectionAdapter
    {
        IInjectionAdapter Bind<T1, T2>() where T2 : T1;

        IInjectionAdapter Bind<T1, T2>(InjectionScope scope) where T2 : T1;

        IInjectionAdapter Bind<T1, T2>(IDictionary<string, object> constructorArguments) where T2 : T1;

        IInjectionAdapter Unbind<T1>();

        T1 TryGet<T1>();

        IInjectionAdapter Bind<T1, T2>(string contextualBindingParameter) where T2 : T1;
    }
}
