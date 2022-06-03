using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment.Editor
{
    public static class TypeUtil
    {
        public static bool IsCollection(Type type)
        {
            if (type.IsArray)
                return true;

            if (type.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();

                if (genericType == typeof(List<>))
                    return true;
                if (genericType == typeof(Dictionary<,>))
                    return true;
            }

            return false;
        }

        public static bool IsArray(Type type) => type.IsArray;

        public static bool IsList(Type type) => IsCollection(type) ? type.GetGenericTypeDefinition() == typeof(List<>)
                                                                   : false;

        public static Type GetTypeEvenFromCollection(Type field) //now only from array and list :(
        {
            if (IsCollection(field))
            {
                if (IsArray(field) || IsList(field))
                    return field.GetGenericArguments()[0];
            }

            return field;
        }

        public static object CollectionConverter(Type field, IEnumerable<object> collection)
        {
            if (IsArray(field))
                return collection.ToArray();

            if (IsList(field))
                return collection.ToList();

            return collection;
        }
    }
}