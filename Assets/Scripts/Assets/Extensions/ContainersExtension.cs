using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Molotkoff.AssetManagment
{
    public static class ContainersExtension
    {
        public static Container<T> GetContainer<T>(this MonoBehaviour monobeh, string id)
        {
            return Containers.GetContainer<T>(new ContainerSettings() { Type = monobeh.GetType(), ID = id }, 
                                              monobeh.gameObject, 0);
        }
    }
}