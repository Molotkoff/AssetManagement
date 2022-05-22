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
    [CreateAssetMenu(menuName = "Game/Assets/Manager/Create")]
    public class AssetManager : ScriptableSingleton<AssetManager>
    {
        [SerializeField] private BaseAssetManager[] _assetsManagers;
        private Dictionary<Type, BaseAssetManager> _assetsManagersCache;
        
        public R Create<S, R>(S settings)
        {
            var type = typeof(S);

            if (_assetsManagersCache == null)
                _assetsManagersCache = new Dictionary<Type, BaseAssetManager>();

            if (!_assetsManagersCache.TryGetValue(type, out var manager))
            {
                foreach (var _assetManager in _assetsManagers) 
                {
                    if (_assetManager is AssetManager<S, R>)
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
            var assetManager = (AssetManager<S, R>)manager;
            return assetManager.Create(settings);
        }

#if UNITY_EDITOR
        public static bool Save()
        {
            var instance = AssetManager.instance;

            if (instance != null)
            {
                instance.Save(true);
                return true;
            }

            return false;
        }
#endif

        [MenuItem("Game/Test")]
        public static void Test()
        {

        }

    }

    [Serializable]
    public class BaseAssetManager { }

    [Serializable]
    public abstract class AssetManager<S, R> : BaseAssetManager
    {
        public abstract R Create(S settings);
    }
}