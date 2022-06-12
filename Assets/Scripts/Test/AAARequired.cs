using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Molotkoff.AssetManagment;
using UnityEngine;

namespace Molotkoff.Test
{
    [Asset("MyAssets")]
    [CreateAssetMenu(menuName = "Game/CreateTest/AAA")]
    public class AAARequired : ScriptableObject
    {
        public int ihaveInt;
    }
}