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
        private SerializedProperty _assetManagers;
        
        private void OnEnable()
        {
            this._assetManagers = serializedObject.FindProperty("_assetsManagers");
        }

        public override void OnInspectorGUI()
        {
            for (int i = 0; i < _assetManagers.arraySize; i++)
            {
                var assetManagerProperty = _assetManagers.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(assetManagerProperty);
            }

            if (GUILayout.Button("Rebuild"))
            {
                AssetManagementBuilder.RebuildAssetManager();
            }
        }
    }
}