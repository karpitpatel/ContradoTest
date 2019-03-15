using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contrado.Core.Data.Context;

namespace Contrado.Common.Data.Context
{
    public interface ICommonEntities : IDbContext
    {
       // IDbSet<ActiveToken> ActiveToken { get; set; }
    }
}
