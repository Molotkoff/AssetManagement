using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Molotkoff.AssetManagment;

namespace Molotkoff.Test
{
    public class AAAManager : AssetManagment.FactoryManager<int, int>
    {
        [Molotkoff.AssetManagment.Dependency(AssetManagment.RequiredAssetMode.Many)] private AAARequired[] required;

        public override int Create(int settings)
        {
            Debug.Log("I have :" + required.Length);
            return 0;
        }
    }
}