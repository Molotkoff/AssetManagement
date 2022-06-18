using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Molotkoff.AssetManagment;

namespace Molotkoff.Test
{
    [CreateAssetMenu(menuName = "Test/Scheme")]
    public class TestScheme : ContainersScheme
    {
        protected override ContainerSchemeBuilder Scheme()
        {
            return new ContainerSchemeBuilder()
                      .AddProvider(new ContainerSettings() { ID = "test", Type = typeof(TestContainerRequirer) },
                                   new TestProvider());
        }
    }
}