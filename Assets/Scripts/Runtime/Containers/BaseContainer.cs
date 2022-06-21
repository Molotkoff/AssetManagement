using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Molotkoff.AssetManagment
{
    [Serializable]
    public class BaseContainer
    {
        [SerializeField] protected internal string _id;
        [SerializeField] protected internal object _self;
    }
}