using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment
{
    public struct ContainerSettings
    {
        public string ID;
        public Type Type;

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Check(ID, -1235634588);
            hash = hash * 23 + Check(Type, 0);

            return hash;
        }

        private int Check(object obj, int safe)
        {
            if (obj == null)
                return safe;

            return obj.GetHashCode();
        }
    }
}