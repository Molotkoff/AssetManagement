using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment
{
    [Serializable]
    public abstract class FactoryManager<S, R> : BaseManager
    {
        public abstract R Create(S settings);
    }
}