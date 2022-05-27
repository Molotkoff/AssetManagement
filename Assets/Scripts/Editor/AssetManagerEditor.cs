using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;
using Unity.Burst;
using Molotkoff.AssetManagment;
using Molotkoff.AssetManagment.Editor.Builders;

namespace Molotkoff.AssetManagment.Editor
{
    [CustomEditor(typeof(AssetManager))]
    public class AssetManagerEditor : UnityEditor.Editor
    {
        private RequiredAssetField[] _assetFields;
        
        private void OnEnable()
        {
            var allAssets = Util.FindAllWithAttribute(typeof(RequiredAssetAttribute))
                                .Select(type => AssetUtil.FindAsset(type));
            var assetFields = new List<RequiredAssetField>();

            foreach (var asset in allAssets)
            {
                assetFields.Add(new RequiredAssetField()
                {
                    Name = asset.GetType().ToString().Replace("UnityEngine.",""),
                    Object = asset
                });
            }

            _assetFields = assetFields.ToArray();
        }

        public override void OnInspectorGUI()
        {
            for (int i = 0; i < _assetFields.Length; i++)
            {
                var assetField = _assetFields[i];
                EditorGUILayout.BeginHorizontal();
                assetField.Object = (ScriptableObject)EditorGUILayout.ObjectField(assetField.Object, assetField.Object.GetType(), false);
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Rebuild"))
            {
                AssetManagementBuilder.RebuildAssetManager();
            }
        }

        class RequiredAssetField
        {
            public string Name;
            public ScriptableObject Object;
        }
    }
}