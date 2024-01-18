using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Helpers.Interfaces
{
    public interface IPropertyFinderHelper<Entity> where Entity : class
    {
        PropertyInfo FindProperty<Entity>(string propertyName);
    }
}
