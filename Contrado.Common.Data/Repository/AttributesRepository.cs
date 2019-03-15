using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contrado.Common.Data.Model;
using Contrado.Core.Data.Repository;

namespace Contrado.Common.Data.Repository
{
    public interface IAttributesRepository : IRepository<Attributes>
    {

    }
    public class AttributesRepository : BaseRepository<Attributes>, IAttributesRepository
    {
        public AttributesRepository(ICommonUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
                
        }
       

       
    }
}
