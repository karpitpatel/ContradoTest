using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Core.Data.Context
{
    /// <summary>
    /// An abstraction of the standard services offered by DbContext, which
    /// will represent all contexts used in the system, and allow dependent
    /// layers to be unit tested using fakes.
    /// </summary>
    public interface IDbContext : IDisposable
    {
        int SaveChanges();

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set", Justification = "EF convention")]
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
