using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contrado.Common.Data.Context;
using Contrado.Core.Data.Repository;

namespace Contrado.Common.Data.Repository
{
    public interface ICommonUnitOfWork : IUnitOfWork
    {
    }

    /// <summary>
    /// An extension of UnitOfWork that specifically uses an instance of ICommonEntities,
    /// and forms the basis of all repositories that work with common data.
    /// </summary>
    public class CommonUnitOfWork : UnitOfWork<ICommonEntities>, ICommonUnitOfWork
    {
        public CommonUnitOfWork(ICommonEntities context)
            : base(context)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
