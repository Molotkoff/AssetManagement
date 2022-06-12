using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using Molotkoff.AssetManagment;
using Molotkoff.AssetManagment.Editor.Builders;

namespace Molotkoff.AssetManagment.Editor
{
    [CustomEditor(typeof(Manager))]
    public class AssetManagerEditor : UnityEditor.Editor
    {
        private BaseManager[] _assetsManagers;
        private AssetGroup<ScriptableObject> _groups;

        private void OnEnable()
        {
            _assetsManagers = TypeUtil.GetFieldValueFromObject<BaseManager[]>(target, "_assetsManagers");
            _groups = BuildAssetsGroups(AssetUtil.FindAssets(asset => asset.GetType().IsDefined(typeof(AssetAttribute), true)));
        }

        public override void OnInspectorGUI()
        {
            DisplayAssets(_groups);
            DisplayRebuild();
        }

        private void DisplayAssets(AssetGroup<ScriptableObject> group)
        {
            group.IsActive = EditorGUILayout.Foldout(group.IsActive, group.Name, false);
            
            if (group.IsActive)
            {
                var assets = group.Assets;

                for (int i = 0; i < assets.Length; i++)
                {
                    var asset = assets[i];
                    var serializedAsset = new SerializedObject(asset);

                    EditorGUILayout.LabelField(asset.name);

                    var propertyIterator = serializedAsset.GetIterator();
                    while(propertyIterator.NextVisible(true))
                    {
                        if (propertyIterator.name != "m_Script")
                        {
                            EditorGUILayout.PropertyField(propertyIterator);

                            if (serializedAsset.hasModifiedProperties)
                                serializedAsset.ApplyModifiedProperties();
                        }
                    }
                }

                var inners = group.Inner;

                for (int i = 0; i < inners.Length; i++)
                {
                    var inner = inners[i];
                    DisplayAssets(inner);
                }
            }
        }

        private void DisplayRebuild()
        {
            if (GUILayout.Button("Rebuild"))
            {
                AssetManagementBuilder.RebuildAssetManager();
            }
        }

        private AssetGroup<ScriptableObject> BuildAssetsGroups(ScriptableObject[] assets)
        {
            var rootInners = new List<AssetGroup<ScriptableObject>>();
            var rootAssets = new List<ScriptableObject>();
            var rootGroup = new AssetGroup<ScriptableObject>("Assets", rootInners, rootAssets);
            var path = new Dictionary<string, (AssetGroup<ScriptableObject> group, 
                                               List<AssetGroup<ScriptableObject>> inners, 
                                               List<ScriptableObject> assets)>();

            var root = (rootGroup, rootInners, rootAssets);
            path.Add("", root);

            for (int i = 0; i < assets.Length; i++)
            {
                var assetManager = assets[i];
                var group = "";
                if (TypeUtil.AttributeOnClass<AssetAttribute>(assetManager, out var assetGroupAttr))
                {
                    group = assetGroupAttr.AssetGroup;

                    if(!path.ContainsKey(group))
                    {
                        var groupPath = group.Split('/');
                        var currentGroup = path[""];
                        var pathBuilder = new StringBuilder();

                        for (int j = 0; j < groupPath.Length; j++)
                        {
                            var jGroupPath = groupPath[j];
                            var inners = currentGroup.inners;

                            if (j > 0)
                                pathBuilder.Append('/');
                            pathBuilder.Append(jGroupPath);

                            var _groupPath = pathBuilder.ToString();

                            if (!inners.Exists(group => group.Name == jGroupPath))
                            {
                                var _assets = new List<ScriptableObject>();
                                var _inner = new List<AssetGroup<ScriptableObject>>();
                                var _group = new AssetGroup<ScriptableObject>(jGroupPath, _inner, _assets);

                                inners.Add(_group);
                                path.Add(_groupPath, (_group, _inner, _assets));
                            }

                            currentGroup = path[_groupPath];
                        }
                    }
                }

                path[group].assets.Add(assetManager);
            }

            return rootGroup;
        }

        class AssetGroup<T>
        {
            public bool IsActive;

            private string _name;
            private List<AssetGroup<T>> _inner;
            private List<T> _assets;

            public string Name => _name;
            public AssetGroup<T>[] Inner => _inner.ToArray();
            public T[] Assets => _assets.ToArray();

            public AssetGroup(string name, List<AssetGroup<T>> inner, List<T> assets)
            {
                this._name = name;
                this._inner = inner;
                this._assets = assets;
            }
        }
    }
}