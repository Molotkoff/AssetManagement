using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Molotkoff.AssetManagment
{
    public static class AssetUtil
    {
        public static ScriptableObject FindAsset(Type onType)
        {
            var onFormat = onType.ToString().Replace("UnityEngine.", "");
            var guids = AssetDatabase.FindAssets($"t:{onFormat}");

            if (guids.Length != 1)
                throw new Exception($"No match assets on type: {onType}");

            var guid = guids[0];
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);

            return asset;
        }

        public static IEnumerable<ScriptableObject> FindAssets(Type onType)
        {
            var assets = new List<ScriptableObject>();
            var onFormat = onType.ToString().Replace("UnityEngine.", "");
            var guids = AssetDatabase.FindAssets($"t:{onFormat}");

            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);

                if (asset != null)
                    assets.Add(asset);
            }

            return assets;
        }

        public static ScriptableObject[] FindAssets(Predicate<ScriptableObject> predicate)
        {
            var assets = new List<ScriptableObject>();
            var guids = AssetDatabase.FindAssets($"t:ScriptableObject");

            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);

                if (asset != null)
                    if (predicate(asset))
                        assets.Add(asset);
            }

            return assets.ToArray();
        }

        public static bool HasAsset(Predicate<Type> predicate)
        {
            return true;
        }
    }
}