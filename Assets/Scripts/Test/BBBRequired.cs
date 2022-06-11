using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Molotkoff.AssetManagment;

namespace Molotkoff.Test
{
    [Asset("MyAssets/BBB")]
    [CreateAssetMenu(menuName = "Game/CreateTest/BBB")]
    public class BBBRequired : ScriptableObject
    {
    }
}