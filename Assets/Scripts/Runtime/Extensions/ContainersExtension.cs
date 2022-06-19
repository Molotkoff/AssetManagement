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
        public static Container<T> GetContainer<T>(this GameObject go, GameObject root, string id)
        {
            return Containers.GetContainer<T>(new ContainerSettings(id, go, root), 0);
        }
    }
}