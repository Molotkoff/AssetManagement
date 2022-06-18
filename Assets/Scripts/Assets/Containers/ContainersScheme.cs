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
        private Dictionary<int, object> _providers;
        private bool _hasBuilded;

        protected abstract ContainerSchemeBuilder Scheme();

        internal T Provide<T>(ContainerSettings settings)
        {
            if (!_hasBuilded)
                Build();

            var hash = settings.GetHashCode();

            if (!_providers.TryGetValue(hash, out var provider))
                throw new Exception("No Provided");

            var tProvider = (IContainerProvider<T>)provider;

            return tProvider.Provide();
        }

        private void Build()
        {
            _hasBuilded = true;

            _providers = new Dictionary<int, object>();

            var schemes = Scheme().Schemes;

            for (int i = 0; i < schemes.Count; i++)
            {
                var scheme = schemes[i];
                _providers.Add(scheme.Item1.GetHashCode(), scheme.Item2);
            }
        }
    }
}