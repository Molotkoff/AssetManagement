using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotkoff.AssetManagment
{
    internal sealed class ScriptBuilder
    {
        internal static event Action OnRebuild;

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