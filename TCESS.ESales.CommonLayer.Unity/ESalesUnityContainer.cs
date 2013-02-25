#region Namespace

using System;
using System.Linq;
using Microsoft.Practices.Unity;

#endregion

namespace TCESS.ESales.CommonLayer.Unity
{
    public class ESalesUnityContainer
    {
        private static IUnityContainer _container = null;
        
        public static void InitializeContainer()
        {            
            _container = new UnityContainer();
        }

        public static IUnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    InitializeContainer();
                }
                return _container;
            }
        }

        public static T Resolve<T>()
        {
            if (Container.IsRegistered(typeof(T)))
            {
                return Container.Resolve<T>();
            }
            else
            {
                return default(T);
            }
        }

        public static T Resolve<T>(string name)
        {
            if (Container.IsRegistered(typeof(T), name))
            {
                return Container.Resolve<T>(name, new ResolverOverride[] { });
            }
            else
            {
                return default(T);
            }
        }

        public static void Register(Type from, Type to)
        {
            if (Container.Registrations.Any(c => c.MappedToType == to && c.RegisteredType == from) == false)
            {
                Container.RegisterType(from, to);
            }
        }

        public static void RegisterInstance<T>(string name, T objectToRegister)
        {
            Container.RegisterInstance<T>(name, objectToRegister);
        }
    }
}