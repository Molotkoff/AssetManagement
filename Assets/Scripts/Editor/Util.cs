using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Molotkoff.AssetManagment
{
    public static class Util
    {
        public static IEnumerable<Type> GetAllInheritedTypes(Type type)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                  .SelectMany(assembly => assembly.GetTypes())
                  .Where(type => type.IsSubclassOf(type) && !type.IsAbstract);
        }

        public static IEnumerable<Type> FindAllWithAttribute(Type attribute)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(assembly => assembly.GetTypes())
                            .Where(type => type.IsDefined(attribute, false) && !type.IsAbstract);
        }
    }
}