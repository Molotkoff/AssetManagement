using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Molotkoff.AssetManagment
{
    public struct ContainerSettings
    {
        private string _id;
        private GameObject _object;
        private GameObject _root;

        public GameObject Root => _root;

        public ContainerSettings(string id, GameObject _object, GameObject root)
        {
            this._id = id;
            this._object = _object;
            this._root = root;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Check(_id, -1235634588);
            hash = hash * 23 + Check(_object, 0);
            hash = hash * 23 + Check(_root, 0);

            return hash;
        }

        private int Check(object obj, int safe)
        {
            if (obj == null)
                return safe;

            return obj.GetHashCode();
        }
    }
}