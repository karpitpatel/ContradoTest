using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contrado.Core.Data.Model;

namespace Contrado.Common.Data.Model
{
    public class Category :BaseEntity
    {
        public long CategoryId { get; set; }

        public string Name { get; set; }

    }
}
