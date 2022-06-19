using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Molotkoff.AssetManagment
{
    public class DependencyAttribute : Attribute
    {
        private RequiredAssetMode _mode;
        public RequiredAssetMode Mode => _mode;

        public DependencyAttribute(RequiredAssetMode mode = RequiredAssetMode.Single)
        {
            this._mode = mode;
        }
    }
}