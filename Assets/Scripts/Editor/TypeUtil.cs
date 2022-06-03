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

        public static bool IsList(Type type) => type.IsGenericType && 
                                                type.GetGenericTypeDefinition() == typeof(List<>);

        public static Type GetTypeEvenFromCollection(Type field) //now only from array and list :(
        {
            if (IsList(field))
                return field.GetGenericArguments()[0];
            else if (IsArray(field))
                return field.GetElementType();

            return field;
        }

        public static object CollectionConverter(Type field, IEnumerable<object> collection)
        {
            if (IsArray(field))
            {
                var arrayType = field.GetElementType();
                var array = Array.CreateInstance(arrayType, collection.Count());
                var i = 0;

                foreach (var item in collection)
                    array.SetValue(item, i++);

                return array;
            }

            if (IsList(field))
            {
                var fieldType = field.GetGenericArguments()[0];
                var listType = typeof(List<>);
                var listArg = new Type[] { fieldType };
                var genericList = listType.MakeGenericType(listArg);
                return Activator.CreateInstance(genericList, new object[] { collection });
            }

            return collection;
        }
    }
}