using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment.Containers.Events
{
    public interface IEventListener
    {
        void OnEventListen(EventContainer container);
    }
}