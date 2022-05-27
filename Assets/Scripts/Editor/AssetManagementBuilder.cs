using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using Unity.Burst;

namespace Molotkoff.AssetManagment.Editor.Builders
{
    public static class AssetManagementBuilder
    {
        [UnityEditor.Callbacks.DidReloadScripts]
        public static void RebuildAssetManager()
        {
            ReconfigureAssetManager(null);
        }

        public static bool BuildRequiredAsset(ScriptableObject requiredAsset)
        {
            var assetManagerObject = AssetManager.instance;

            if (assetManagerObject != null)
            {
                var requiredAssetType = requiredAsset.GetType();
                var assetManagerObjectType = assetManagerObject.GetType();
                var assetManagerObjectField = assetManagerObjectType.GetField("_assetsManagers", BindingFlags.Instance | BindingFlags.NonPublic);
                var assetManagersObjects = (object[])assetManagerObjectField.GetValue(assetManagerObject);

                foreach (var assetManager in assetManagersObjects)
                {
                    var assetManagerType = assetManager.GetType();
                    var bindings = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                    var assetManagerRequiredFields = assetManagerType.GetMembers(bindings)
                                                    .Where(member => member.MemberType == MemberTypes.Field)
                                                    .Where(member => member.IsDefined(typeof(RequireAttribute), false))
                                                    .Select(member => (FieldInfo)member);

                    foreach (var assetManagerRequiredField in assetManagerRequiredFields)
                    {
                        if (assetManagerRequiredField.FieldType == requiredAssetType)
                        {
                            assetManagerRequiredField.SetValue(assetManager, requiredAsset);
                        }
                    }
                }

                return true;
            }

            return false;
        }

        private static BaseAssetManager[] ReconfigureAssetManager(SerializedObject serializedObject)
        {
            var assetManagerObject = AssetManager.instance;
            var assetManagerObjectType = assetManagerObject.GetType();
            var assetManagerObjectField = assetManagerObjectType.GetField("_assetsManagers", BindingFlags.Instance | BindingFlags.NonPublic);

            if (serializedObject != null)
                Undo.RecordObject(assetManagerObject, "Configured Asset-Manager");

            var assets = FindAssetManagers();
            var cache = new Dictionary<Type, object>();
            foreach (var asset in assets)
                ConfiguteAssetManager(asset, assets, cache);

            assetManagerObjectField.SetValue(assetManagerObject, assets);

            return assets;
        }

        private static BaseAssetManager[] FindAssetManagers()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                  .SelectMany(assembly => assembly.GetTypes())
                  .Where(type => type.IsSubclassOf(typeof(BaseAssetManager)) && !type.IsAbstract)
                  .Select(type => Activator.CreateInstance(type) as BaseAssetManager)
                  .ToArray();
        }

        private static void ConfiguteAssetManager(BaseAssetManager assetManager, BaseAssetManager[] assetManagers, Dictionary<Type, object> cache)
        {
            var fields = GetFields(assetManager, field => field.IsDefined(typeof(RequireAttribute), true));
            foreach (var field in fields)
            {
                var requiredType = field.FieldType;

                if (!cache.TryGetValue(requiredType, out var requiredAsset))
                {
                    var foundAssets = AssetUtil.FindAssets(requiredType);

                    if (foundAssets.Length != 1)
                        Debug.LogError("Warning");

                    requiredAsset = foundAssets[0];
                    cache.Add(requiredType, requiredAsset);
                }

                field.SetValue(assetManager, requiredAsset);
            }
        }

        private static FieldInfo[] GetFields(object onObject, Predicate<FieldInfo> predicate)
        {
            var onType = onObject.GetType();
            var bindings = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            return onType.GetMembers(bindings)
                         .Where(member => member.MemberType == MemberTypes.Field)
                         .Select(member => (FieldInfo)member)
                         .ToArray();
        }
    }
}