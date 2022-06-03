using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Molotkoff.Test
{
    public class Mono : MonoBehaviour
    {
        public void Awake()
        {
            AssetManagment.AssetManager.instance.Create<int, int>(5);
        }
    }
}