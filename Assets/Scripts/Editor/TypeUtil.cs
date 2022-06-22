using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Molotkoff.AssetManagment
{
    public static class TypeUtil
    {
        public static bool AttributeOnClass<A>(object obj, out A attribute) where A : Attribute
        {
            var type = obj.GetType();
            attribute = type.GetCustomAttribute<A>(true);

            return attribute != null;
        }

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

        public static T GetFieldValueFromObject<T>(object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | 
                                                          BindingFlags.Public    |
                                                          BindingFlags.Instance);

            if (field == null)
                throw new MissingFieldException($"No field in {obj} with name {fieldName}");

            return (T)field.GetValue(obj);
        }

        public static T GetFieldValueFromObjectPreCast<T, C>(object obj, string fieldName)
        {
            var field = typeof(C).GetField(fieldName, BindingFlags.NonPublic |
                                                      BindingFlags.Public    |
                                                      BindingFlags.Instance);

            if (field == null)
                throw new MissingFieldException($"No field in {obj} with name {fieldName} on type {typeof(C)}");

            //var inspectedType = typeof(T);
            //var fieldType = field.FieldType;

            return (T)field.GetValue(obj);
        }

        public static void SetFieldValueFromObject<T>(object obj, string fieldName, T _value)
        {
            var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic |
                                                          BindingFlags.Instance);

            if (field == null)
                throw new MissingFieldException($"No field in object");

            var inspectedType = typeof(T);
            var fieldType = field.FieldType;

            if (inspectedType != fieldType)
                throw new MissingFieldException($"No field in object");

            field.SetValue(obj, _value);
        }

        public static void SetFieldValueInSerializibleProperty<T>(SerializedProperty property, string fieldName, T _value)
        {
            SetFieldValueFromObject(property.objectReferenceValue, fieldName, _value);
        }

        public static T GetFieldValueInSerializibleProperty<T>(SerializedProperty property, string fieldName)
        {
            return GetFieldValueFromObject<T>(property.objectReferenceValue, fieldName);
        }

        public static Type GetFieldTypeFromSerializibleProperty(SerializedProperty property)
        {
            var targetType = property.serializedObject.targetObject.GetType();
            var fieldInfo = targetType.GetField(property.name, BindingFlags.NonPublic |
                                                               BindingFlags.Public |
                                                               BindingFlags.Instance);
            return fieldInfo.FieldType;
        }
    }
}