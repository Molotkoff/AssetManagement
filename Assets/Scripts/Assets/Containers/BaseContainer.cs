using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment
{
    public class BaseContainer : IDisposable
    {
        public void Dispose()
        {
            Containers.FreeContainer(this);
        }
    }
}