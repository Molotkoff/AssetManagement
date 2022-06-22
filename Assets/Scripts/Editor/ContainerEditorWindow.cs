using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace Molotkoff.AssetManagment
{
    public class ContainerEditorWindow : EditorWindow
    {
        [MenuItem("Molotkoff/AssetManagment/Containers")]
        public static void ShowWindow()
        {
            var window = GetWindow<ContainerEditorWindow>();
            window.Focus();
        }

        private void OnEnable()
        {
            
        }

        private void OnGUI()
        {
            //window
        }
    }
}