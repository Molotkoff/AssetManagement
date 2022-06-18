using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Molotkoff.AssetManagment;

namespace Molotkoff.Test
{
    public class TestProvider : IContainerProvider<int>
    {
        public int Provide()
        {
            return 0;
        }
    }
}