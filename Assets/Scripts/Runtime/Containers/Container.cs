using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment
{
    [Serializable]
    public class Container<T> : BaseContainer, IDisposable
    {
        public T Value;

        public Container(T _value)
        {
            this.Value = _value;
        }
    }
}