using Ninject;
using Ninject.Parameters;
using Ninject.Extensions.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GEPED.Utils
{
    public static class Resolver
    {
        private static IKernel kernel;
        public static string NamespacePrefixDefaults { get; private set; }

        private static Type FindType(string typename)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name.StartsWith(NamespacePrefixDefaults));
            foreach (var assembly in assemblies)
            {
                Type type = assembly.GetTypes().Where(t => t.Name.Equals(typename)).FirstOrDefault();

                if (type != null)
                {
                    return type;
                }
            }
            return null;

        }
        
        public static void Setup(IList<Assembly> assemblies, string namespacePrefixDefault)
        {
            NamespacePrefixDefaults = namespacePrefixDefault;
            if (kernel != null)
                kernel.Dispose();

            kernel = new StandardKernel();

            kernel.Bind(x => x
                            .From(assemblies)
                            .SelectAllClasses()
                            .BindAllInterfaces());
        }

        public static void Register<TInterface, TImplementation>()
        {
            ValidateContainer();
            kernel.Bind<TInterface>().To(typeof(TImplementation)).InSingletonScope();
        }

        public static void Register<TInterface>(Func<Ninject.Activation.IContext, TInterface> method)
        {
            ValidateContainer();
            kernel.Bind<TInterface>().ToMethod(method).InSingletonScope();
        }

        private static void RemoveBindings(Type TInterface)
        {
            var binds = kernel.GetBindings(TInterface);

            foreach (var bindRemove in binds)
            {
                kernel.RemoveBinding(bindRemove);
            }
        }

        public static void AddBindings(Dictionary<Type, Type> bindings)
        {
            foreach (var binding in bindings)
            {
                RemoveBindings(binding.Key);
                var fromType = binding.Key;
                var toType = binding.Value;
                kernel.Bind(fromType).To(toType).WithMetadata("type", toType.Name.ToString());
            }
        }
        /// <summary>
        /// Retorna uma instância do tipo informado, obtendo a instância
        /// do container de IoC (Ninject)
        /// </summary>
        public static T Get<T>(params object[] values)
        {
            ValidateContainer();

            if (values == null || !values.Any())

                return kernel.Get<T>();

            else
            {
                string typename = kernel.GetBindings(typeof(T)).Select(x => x.Metadata.Get<string>("type")).FirstOrDefault();
                if (string.IsNullOrEmpty(typename))
                {
                    return kernel.Get<T>();
                }

                var binded = FindType(typename);

                if (binded == null)
                    throw new Exception("");

                ConstructorInfo[] allConst = binded.GetConstructors();

                ConstructorInfo matchedConst = allConst.FirstOrDefault(c => IsMatch(c.GetParameters(), values));

                if (matchedConst == null)
                    return default(T);

                IParameter[] arg = GetConstructorArguments(matchedConst, values);

                return kernel.Get<T>(arg);
            }
        }

        private static ConstructorArgument[] GetConstructorArguments(ConstructorInfo ctr, params object[] values)
        {
            return ctr.GetParameters().Zip(values, (ctrParam, value) => new ConstructorArgument(ctrParam.Name, value)).ToArray();
        }

        private static bool IsMatch(ParameterInfo[] parameterTypes, params object[] values)
        {
            if (parameterTypes.Length == values.Length)
            {
                for (int i = 0; i < parameterTypes.Length; i++)
                {

                    if (parameterTypes[i].ParameterType.IsInterface)
                    {

                        if (parameterTypes[i].ParameterType != values[i].GetType().GetInterface(parameterTypes[i].ParameterType.Name))
                        {
                            return false;
                        }
                    }
                    else if (parameterTypes[i].ParameterType != values[i].GetType())
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public static void Inject(object instance)
        {
            ValidateContainer();
            kernel.Inject(instance);
        }

        private static void ValidateContainer()
        {
            if (kernel == null)
            {
                throw new InvalidOperationException("O container não foi inicializado. Execute o método DependencyInjectionContainer.Setup() antes de utilizar o container");
            }
        }
    }
}
