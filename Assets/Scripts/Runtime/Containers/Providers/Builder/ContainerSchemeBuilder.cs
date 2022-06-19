using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment
{
    public sealed class ContainerSchemeBuilder
    {
        private List<(string, object)> _schemes = new List<(string, object)>();

        internal List<(string, object)> Schemes => _schemes;

        public void AddProvider<T>(string settings, IContainerProvider<T> provider)
        {
            _schemes.Add((settings, provider));

            //return this;
        }
    }
}