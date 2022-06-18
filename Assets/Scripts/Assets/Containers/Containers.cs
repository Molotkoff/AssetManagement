using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Molotkoff.AssetManagment
{
    internal class Containers
    {
        private static Containers _instance;

        private ContainersScheme _scheme;
        private Dictionary<GameObject, ContainerHandler> _handlers = new Dictionary<GameObject, ContainerHandler>();

        private static Containers Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Containers();
                    _instance._scheme = AssetManagment.instance._containerScheme;
                }

                return _instance;
            }
        }

        internal static Container<T> GetContainer<T>(ContainerSettings settings, GameObject obj, int local_id)
        {
            var inst = Instance;

            if (!inst._handlers.TryGetValue(obj, out var handler))
            {
                var tValue = inst._scheme.Provide<T>(settings);
                var container = new Container<T>(tValue);
                handler = new ContainerHandler(container);
            }

            return (Container<T>)handler.Container;
        }

        internal static void FreeContainer(BaseContainer container)
        {

        }
    }
}