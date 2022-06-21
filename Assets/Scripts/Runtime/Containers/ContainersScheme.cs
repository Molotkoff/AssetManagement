using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Molotkoff.AssetManagment
{
    public abstract class ContainersScheme : ScriptableObject
    {
        internal Dictionary<string, Dictionary<Type, ContainerSettings>> _settings =
            new Dictionary<string, Dictionary<Type, ContainerSettings>>();

        internal bool TryGetSettings(string id, Type type, out ContainerSettings settings)
        {
            if (!_settings.TryGetValue(id, out var settings0))
            {
                settings = null;
                return false;
            }

            return settings0.TryGetValue(type, out settings);
        }

        protected ContainersScheme AddContainer<T>(string id, ContainerScope scope, IContainerProvider<T> provider)
        {
            var settings = new ContainerSettings()
            {
                id = id,
                Scope = scope,
                Provider = provider
            };
            
            if (!_settings.TryGetValue(id, out var _contanerSettings))
            {
                _contanerSettings = new Dictionary<Type, ContainerSettings>();
                _settings.Add(id, _contanerSettings);
            }

            _contanerSettings.Add(typeof(T), settings);
            
            return this;
        }

        protected ContainersScheme AddContainer<T>(string id, ContainerScope scope, Func<T> provider)
        {
            var settings = new ContainerSettings()
            {
                id = id,
                Scope = scope,
                Provider = new AnonymousContainerProvider<T>(provider)
            };

            if (!_settings.TryGetValue(id, out var _contanerSettings))
            {
                _contanerSettings = new Dictionary<Type, ContainerSettings>();
                _settings.Add(id, _contanerSettings);
            }

            _contanerSettings.Add(typeof(T), settings);

            return this;
        }
    }
}