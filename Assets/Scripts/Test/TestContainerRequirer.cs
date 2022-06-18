using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Molotkoff.AssetManagment;

namespace Molotkoff.Test
{
    public class TestContainerRequirer : MonoBehaviour
    {
        private Container<int> _containerWithInt;

        private void Awake()
        {
            _containerWithInt = this.GetContainer<int>("test");
            Debug.Log(_containerWithInt.Value);
        }
    }
}