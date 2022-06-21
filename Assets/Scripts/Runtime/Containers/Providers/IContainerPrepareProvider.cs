using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment
{
    public interface IContainerPrepareProvider<S>
    {
        void Prepare(S settings);
    }
}