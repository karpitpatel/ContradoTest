using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Services.Core.Mapping
{
    /// <summary>
    /// Defines a type that can perform a simple field-by-field mapping between
    /// one type of object and another. Mappers generally rely only on the
    /// information & fields within the objects being mapped, and not on any
    /// external calculations or retrieval.
    /// </summary>
    /// <typeparam name="TSrc">the "source" type of the operation - the item copied from</typeparam>
    /// <typeparam name="TDst">the "destination" type of the operation - the item copied to</typeparam>
    public interface IModelMapper<TSrc, TDst>
    {
        TDst Map(TSrc src);

        TDst Map(TSrc src, TDst dst);

        TSrc Map(TDst dst);

        TSrc Map(TDst dst, TSrc src);
    }
}
