using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Molotkoff.AssetManagment
{
    [Serializable]
    public abstract class ContainersSchemeDefenition : ScriptableObject
    {
        public abstract ContainersScheme Scheme();
    }
}