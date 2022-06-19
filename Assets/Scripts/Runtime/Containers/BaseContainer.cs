using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Molotkoff.AssetManagment
{
    [Serializable]
    public class BaseContainer : IDisposable
    {
        [SerializeField] private string _containerName;
        
        public void Dispose()
        {
            Containers.FreeContainer(this);
        }
    }
}