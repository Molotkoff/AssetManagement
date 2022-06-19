using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace Molotkoff.AssetManagment.Editor
{
    public class ContainerEditorWindow : EditorWindow
    {
        [MenuItem("Window/My Window")]
        public static void ShowWindow()
        {
            var window = GetWindow<ContainerEditorWindow>();
            window.Focus();
        }

        private void OnEnable()
        {
            
        }
    }
}