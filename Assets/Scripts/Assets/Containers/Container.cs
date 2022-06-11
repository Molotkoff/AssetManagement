using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment.Containers
{
    public class Container
    {
        private string _name;
        private Container _parent;

        public Container(Container parent)
        {
            this._parent = parent;
        }

        public Container(Container parent, string name)
        {

        }
    }
}