using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Contrado.Core.Injection
{
    /// <summary>
    /// A utility class that can be used to load all IInjectionConfiguration
    /// instances for an application, which can be used to initialise the various
    /// bindings, etc., for the application's IoC container. Each instance of
    /// IInjectionConfiguration should correspond to a logical collection of
    /// injectable items (e.g.: for an assembly), and all such instances are
    /// discovered and combined into a single Configurations property here.
    /// </summary>
    public class InjectionConfigurationLoader
    {
        /// <summary>
        /// Creates a new InjectionConfigurationLoader that looks for parts in
        /// the location provided. Composition of these parts occurs within this
        /// constructor.
        /// </summary>
        /// <param name="path">the path on which to look for components/parts</param>
        public InjectionConfigurationLoader(string path)
        {
            var directoryCatalog = new DirectoryCatalog(path);
            var catalog = new AggregateCatalog(directoryCatalog);
            Container = new CompositionContainer(catalog);
            Container.ComposeParts(this);
        }

        /// <summary>
        /// Endpoint for the various configurations that have been loaded into
        /// our container. Each configuration should handle the injection
        /// initialisation for a logical collection of items (e.g.: for a single
        /// assembly).
        /// </summary>
        [ImportMany]
        public IEnumerable<IInjectionConfiguration> Configurations { get; set; }

        /// <summary>
        /// Contains all of our configuration components/parts. Initialised during
        /// construction from a catalog that is rooted in the path provided.
        /// </summary>
        private CompositionContainer Container { get; set; }
    }
}
