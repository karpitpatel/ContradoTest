using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contrado.Common.Data.Model;
using Contrado.Core.Data.Mapping;

namespace Contrado.Common.Data.Mapping
{
    public class CategoryMap : BaseEntityMap<Category>
    {
        public CategoryMap()
        {
            // Table and column mapping
            ToTable("Category");

            // Primary key
            HasKey(t => t.CategoryId);
        }
    }
}
