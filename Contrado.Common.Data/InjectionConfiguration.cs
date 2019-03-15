using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contrado.Common.Data.Context;
using Contrado.Common.Data.Repository;
using Contrado.Core.Injection;

namespace Contrado.Common.Data
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

                 .Bind<ICommonEntities, CommonEntities>(InjectionScope.Request)
                .Bind<ICommonUnitOfWork, CommonUnitOfWork>(InjectionScope.Request)

                // First-class repository services.
                .Bind<IAttributesRepository, AttributesRepository>();


        }
    }
}
