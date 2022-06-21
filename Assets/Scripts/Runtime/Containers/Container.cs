using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment
{
    [Serializable]
    public class Container<T> : BaseContainer
    {
        private T _value;
        private bool _needInit;

        public T Value
        {
            get
            {
                if (_needInit)
                {
                    _value = Containers.Get<T>(_id, _self);
                    _needInit = false;
                }

                return _value;
            }
        }
    }
}