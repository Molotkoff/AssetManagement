using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment
{
    public class AnonymousContainerProvider<T> : IContainerProvider<T>
    {
        private Func<T> _provider;

        public AnonymousContainerProvider(Func<T> provider) 
        {
            this._provider = provider;
        }

        public T Provide()
        {
            return _provider();
        }
    }
}