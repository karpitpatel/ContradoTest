using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contrado.Common.Data.Model;
using Contrado.Core.Data.Mapping;

namespace Contrado.Common.Data.Mapping
{
    public class AttributeMap : BaseEntityMap<Attributes>
    {
        public AttributeMap()
        {
            // Table and column mapping
            ToTable("Attribute");

            // Primary key
            HasKey(t => t.AttributesId);
        }
    }
}
