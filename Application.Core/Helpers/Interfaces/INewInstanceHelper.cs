using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Helpers.Interfaces
{
    public interface INewInstanceHelper
    {
        public Dto CreateInstance<Dto>();
    }
}
