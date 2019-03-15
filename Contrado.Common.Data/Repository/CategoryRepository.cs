using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contrado.Common.Data.Model;
using Contrado.Core.Data.Repository;

namespace Contrado.Common.Data.Repository
{
   
    public interface ICategoryRepository : IRepository<Category>
    {

    }
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ICommonUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
