using Contrado.Common.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Common.Domain.Manager
{
    public interface IAttributeManager
    {

    }
   public class AttributeManager : IAttributeManager
    {
        private readonly IAttributesRepository _attributesRepository;
        public AttributeManager(IAttributesRepository attributesRepository)
        {
            _attributesRepository = attributesRepository;
        }
    }
}
