using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Molotkoff.AssetManagment
{
    [Serializable]
    public class ContainersScheme
    {
        internal Dictionary<string, Dictionary<Type, ContainerSettings>> _settings =
            new Dictionary<string, Dictionary<Type, ContainerSettings>>();

        private Dictionary<Type, List<string>> _containers = 
            new Dictionary<Type, List<string>>();

        internal bool TryGetSettings(string id, Type type, out ContainerSettings settings)
        {
            if (!_settings.TryGetValue(id, out var settings0))
            {
                settings = null;
                return false;
            }

            return settings0.TryGetValue(type, out settings);
        }

        public string[] GetContainers(Type type)
        {
            if (_containers.TryGetValue(type, out var containers))
                return containers.ToArray();

            return new string[1] { "None" };
        }

        public string AddContainer<T>(string name, ContainerScope scope, IContainerProvider<T> provider)
        {
            var id = new Guid().ToString();
            var containerType = typeof(T);

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

            _contanerSettings.Add(containerType, settings);

            if (!_containers.TryGetValue(containerType, out var containers))
            {
                containers = new List<string>();
                _containers.Add(containerType, containers);
            }

            containers.Add(name);

            return id;
        }

        public string AddContainer<T>(string name, ContainerScope scope, Func<T> provider)
        {
            var id = new Guid().ToString();
            var containerType = typeof(T);

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

            _contanerSettings.Add(containerType, settings);

            if (!_containers.TryGetValue(containerType, out var containers))
            {
                containers = new List<string>();
                _containers.Add(containerType, containers);
            }

            containers.Add(name);

            return id;
        }
    }
}