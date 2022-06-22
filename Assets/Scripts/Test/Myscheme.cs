using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Molotkoff.AssetManagment;

namespace Molotkoff.Test
{
    [CreateAssetMenu(menuName = "Test/MyScheme")]
    public class Myscheme : ContainersSchemeDefenition
    {
        public override ContainersScheme Scheme()
        {
            return new ContainersScheme();
        }
    }
}