using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment
{
    internal class ContainerHandler
    {
        public BaseContainer Container { get; private set; }

        public ContainerHandler(BaseContainer container)
        {
            Container = container;
        }
    }
}