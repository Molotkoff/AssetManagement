using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Molotkoff.AssetManagment
{
    internal class Containers
    {
        private static Containers _instance;

        private ContainersScheme _scheme;

        private Dictionary<string, Dictionary<Type, object>> _consistent;
        private Dictionary<string, Dictionary<object, Dictionary<Type, object>>> _self;

        private static Containers Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Containers()
                    {
                        _scheme = AssetManagment.instance._containerScheme,
                        _consistent = new Dictionary<string, Dictionary<Type, object>>(),
                        _self = new Dictionary<string, Dictionary<object, Dictionary<Type, object>>>()
                    };
                }

                return _instance;
            }
        }

        internal static T Get<T>(string id, object self)
        {
            return (T)Get<T>(id, self, typeof(T), Instance);
        }

        private static object Get<T>(string id, object self, Type type, Containers containers)
        {
            if (!containers._scheme.TryGetSettings(id, type, out var settings))
                throw new Exception("Not found scheme for container");

            switch (settings.Scope)
            {
                case ContainerScope.Consistent:
                    if (!containers._consistent.TryGetValue(id, out var consistentHandler))
                    {
                        consistentHandler = new Dictionary<Type, object>();
                        containers._consistent.Add(id, consistentHandler);
                    }

                    if (!consistentHandler.TryGetValue(type, out var consistentValue))
                    {
                        var containerName = settings.id;
                        if (containerName == id)
                        {
                            var provider = (IContainerProvider<T>)settings.Provider;
                            var provided = provider.Provide();
                            consistentHandler.Add(type, provided);

                            return provided;
                        }

                        var _provided = Get<T>(containerName, self, type, containers);
                        consistentHandler.Add(type, _provided);

                        return _provided;
                    }

                    return consistentValue;
                case ContainerScope.Self:
                    if (!containers._self.TryGetValue(id, out var selfHandler))
                    {
                        selfHandler = new Dictionary<object, Dictionary<Type, object>>();
                        containers._self.Add(id, selfHandler);
                    }

                    if (!selfHandler.TryGetValue(self, out var selfContainer))
                    {
                        selfContainer = new Dictionary<Type, object>();
                        selfHandler.Add(self, selfContainer);
                    }

                    if (!selfContainer.TryGetValue(type, out var selfValue))
                    {
                        var containerName = settings.id;
                        if (containerName == id)
                        {
                            var provider = (IContainerProvider<T>)settings.Provider;
                            var provided = provider.Provide();
                            selfContainer.Add(type, provided);

                            return provided;
                        }

                        var _provided = Get<T>(containerName, self, type, containers);
                        selfContainer.Add(type, _provided);

                        return _provided;
                    }

                    return selfValue;
            }

            return null;
        }
    }
}