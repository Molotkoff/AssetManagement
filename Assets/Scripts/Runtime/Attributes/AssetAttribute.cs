using System;

namespace Molotkoff.AssetManagment
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AssetAttribute : Attribute
    {
        private string _group;

        public string AssetGroup => _group;

        public AssetAttribute(string assetGroup = "")
        {
            this._group = assetGroup;
        }
    }
}