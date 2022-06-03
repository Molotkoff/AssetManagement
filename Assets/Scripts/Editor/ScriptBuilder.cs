using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment.Editor
{
    class ScriptBuilder
    {
        /*
        [UnityEditor.Callbacks.DidReloadScripts]
        public static void BuildScripts()
        {
            var allNotImplementedComponentTypes = Util.GetAllInheritedTypes(typeof(Component<>))
                                                 .Where(component => !AssetUtil.HasAsset(_ => 
                                                 {
                                                     if (component.IsGenericType)
                                                         return component.GetGenericTypeDefinition() == typeof(Component<>);

                                                     return false;
                                                 }));

        }*/
    }
}