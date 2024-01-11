using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Helpers.Interfaces;

namespace Application.Core.Helpers
{
    public class NewInstanceHelper: INewInstanceHelper
    {
        public Dto CreateInstance<Dto>()
        {
            Type dtoType = typeof(Dto);

            object dtoInstance = Activator.CreateInstance(dtoType);

            Dto dto = (Dto)dtoInstance;

            return dto;
        }
    }
}
