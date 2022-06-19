using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using Unity.Burst;

namespace Molotkoff.AssetManagment
{
    [CreateAssetMenu(menuName = "Molotkoff/AssetManagment/Manager")]
    public class AssetManagment : ScriptableSingleton<AssetManagment>
    {
        [SerializeField] internal ContainersScheme _containerScheme;
        [SerializeField] private BaseManager[] _assetsManagers;
        [SerializeField] private ScriptableObject[] _assets;

        private Dictionary<Type, BaseManager> _assetsManagersCache;

#if !UNITY_ENGINE
        private List<string> _containers = new List<string>();
#endif

        public R Create<S, R>(S settings)
        {
            var type = typeof(S);

            if (_assetsManagersCache == null)
                _assetsManagersCache = new Dictionary<Type, BaseManager>();

            if (!_assetsManagersCache.TryGetValue(type, out var manager))
            {
                foreach (var _assetManager in _assetsManagers) 
                {
                    if (_assetManager is FactoryManager<S, R>)
                    {
                        manager = _assetManager;
                        _assetsManagersCache.Add(type, manager);
                        break;
                    }
                }
            }

#if UNITY_EDITOR
            Debug.Assert(manager != null, $"No defined asset-manager of type:{type}");
#endif
            var assetManager = (FactoryManager<S, R>)manager;
            return assetManager.Create(settings);
        }

        /*
#if UNITY_EDITOR
        public static bool Save()
        {
            var instance = AssetManagment.instance;

            if (instance != null)
            {
                instance.Save(true);
                return true;
            }

            return false;
        }
#endif
        */
    }


}