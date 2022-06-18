using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment
{
    public sealed class ContainerSchemeBuilder
    {
        private List<(ContainerSettings, object)> _schemes = new List<(ContainerSettings, object)>();

        internal List<(ContainerSettings, object)> Schemes => _schemes;

        public ContainerSchemeBuilder AddProvider<T>(ContainerSettings settings, IContainerProvider<T> provider)
        {
            _schemes.Add((settings, provider));

            return this;
        }
    }
}