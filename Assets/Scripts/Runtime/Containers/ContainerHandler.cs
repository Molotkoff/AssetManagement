using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment
{
    internal class ContainerHandler
    {
        private Dictionary<Type, BaseContainer> _containers = new Dictionary<Type, BaseContainer>();

        public BaseContainer Container;
    }
}