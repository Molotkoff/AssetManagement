using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Molotkoff.AssetManagment
{
    public class RequireAttribute : Attribute
    {
        private RequiredAssetMode _mode;
        private string _assetGroup;

        public string AssetGroup => _assetGroup;
        public RequiredAssetMode Mode => _mode;

        public RequireAttribute(RequiredAssetMode mode = RequiredAssetMode.Single) : this(string.Empty, mode)
        {

        }

        public RequireAttribute(string assetGroup, RequiredAssetMode mode = RequiredAssetMode.Single)
        {
            this._assetGroup = assetGroup;
            this._mode = mode;
        }
    }
}