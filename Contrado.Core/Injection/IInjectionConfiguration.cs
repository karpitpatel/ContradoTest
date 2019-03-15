using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Core.Injection
{
    /// <summary>
    /// Represents part of the configuration of an application's IoC injection.
    /// As such, implementations should use the IInjectionAdapter supplied to
    /// their Configure method to register all required mappings, etc., for the
    /// set of elements that they represent. Individual implementations may
    /// cover a complete assembly, or some subset of same.
    /// </summary>
    public interface IInjectionConfiguration
    {
        string Name { get; }

        void Configure(IInjectionAdapter injection);
    }
}
