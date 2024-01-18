using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Helpers.Interfaces;

namespace Application.Core.Helpers
{
    public class PropertyFinderHelper<Entity> : IPropertyFinderHelper<Entity> where Entity : class
    {
        public PropertyInfo FindProperty<Entity>(string propertyName)
        {
            Type type = typeof(Entity);
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            return propertyInfo;
        }
    }
}
