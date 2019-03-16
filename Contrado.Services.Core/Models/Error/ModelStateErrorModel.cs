using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Services.Core.Models.Error
{
    public class ModelStateErrorModel
    {
        public string Message { get; set; }

        public ICollection<ModelStateItemModel> ModelState { get; set; }
    }

    public class ModelStateItemModel
    {
        public string Key { get; set; }

        public string Message { get; set; }
    }
}
