using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Services.Core.Mapping
{
    /// <summary>
    /// A base implementation of IModelMapper that currently implements mapping
    /// in terms of AutoMapper. Subclasses, therefore, currently initialise themselves
    /// by registering the required mappings with this library.
    /// </summary>
    /// <typeparam name="TSrc">the "source" object - entity level item</typeparam>
    /// <typeparam name="TDst">the "destination" object - model level item</typeparam>
    public class GenericModelMapper<TSrc, TDst> : IModelMapper<TSrc, TDst>
    {
        /// <summary>
        /// Maps the given source entity to the destination model type
        /// specified for this instance.
        /// </summary>
        /// <param name="src">the source entity to be mapped</param>
        /// <returns>the corresponding destination model for this entity</returns>
        public virtual TDst Map(TSrc src)
        {
            return Mapper.Map<TSrc, TDst>(src);
        }

        /// <summary>
        /// Maps the given destination model to the source entity type
        /// specified for this instance.
        /// </summary>
        /// <param name="dst">the destination-type model to be mapped</param>
        /// <returns>the corresponding entity for this model</returns>
        public virtual TSrc Map(TDst dst)
        {
            return Mapper.Map<TDst, TSrc>(dst);
        }

        /// <summary>
        /// Maps the values from the given source model into the given destination
        /// model, returning the latter. This method can be used to perform an
        /// in-place mapping over the given destination, using the mapping rules
        /// defined for these types of objects.
        /// </summary>
        /// <param name="src">the object to map from</param>
        /// <param name="dst">the object to map into</param>
        /// <returns>the target object (dst) provided, post-operation</returns>
        public virtual TDst Map(TSrc src, TDst dst)
        {
            return Mapper.Map<TSrc, TDst>(src, dst);
        }

        /// <summary>
        /// Maps the values from the given destination model into the given source
        /// model, returning the latter. This method can be used to perform an
        /// in-place mapping over the given source, using the mapping rules that
        /// are defined for these types of objects.
        /// </summary>
        /// <param name="dst">the object to map from</param>
        /// <param name="src">the object to map into</param>
        /// <returns>the target object (src) provided, post-operation</returns>
        public virtual TSrc Map(TDst dst, TSrc src)
        {
            return Mapper.Map<TDst, TSrc>(dst, src);
        }
    }
}
