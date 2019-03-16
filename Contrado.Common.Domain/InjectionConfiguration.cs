using Contrado.Common.Domain.Manager;
using Contrado.Core.Injection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Common.Domain
{
    [Export(typeof(IInjectionConfiguration))]
    public class InjectionConfiguration : IInjectionConfiguration
    {
        public string Name
        {
            get { return GetType().AssemblyQualifiedName; }
        }

        public void Configure(IInjectionAdapter injection)
        {
            if (injection == null) return;

            injection
                .Bind<IAttributeManager, AttributeManager>(InjectionScope.Request);
                
        }
    }
}
