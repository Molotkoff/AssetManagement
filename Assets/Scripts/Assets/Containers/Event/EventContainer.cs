using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment.Containers.Events
{
    public sealed class EventContainer
    {
        public void AddListener<T>(Action<T> listener)
        {

        }

        public void AddListener<T>(Action listener)
        {

        }
    }
}