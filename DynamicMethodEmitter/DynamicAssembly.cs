using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMethodEmitter
{
    public class DynamicAssembly
    {
        private readonly AssemblyName _assemblyName;
        private readonly AssemblyBuilder _assemblyBuilder;
        private readonly ModuleBuilder _moduleBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DynamicAssembly(string name)
        {
            _assemblyName = new AssemblyName(name);
            _assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(_assemblyName,
                AssemblyBuilderAccess.RunAndSave, Directory.GetCurrentDirectory());
            _moduleBuilder = _assemblyBuilder.DefineDynamicModule(_assemblyName.Name, _assemblyName.Name + ".dll");
        }

        public Delegate BuildMethod(string methodName, Type delegateType)
        {
            var methodInfo = delegateType.GetMethod("Invoke");
            TypeBuilder builder = _moduleBuilder.DefineType("Test", TypeAttributes.Public);
            var parameters = methodInfo.GetParameters().Select(x => x.ParameterType).ToList();
            parameters.Insert(0, delegateType);
            var methodBuilder = builder.DefineMethod(methodName, MethodAttributes.Public, methodInfo.ReturnType, parameters.ToArray());
            var il = methodBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldarg_2);
            il.Emit(OpCodes.Call, methodInfo);
            il.Emit(OpCodes.Ret);
            var type = builder.CreateType();
            Type delegateResult = Expression.GetDelegateType(parameters.ToArray());
            return type.GetMethod(methodName).CreateDelegate(delegateResult);
        }

        public void Save()
        {

            _assemblyBuilder.Save(_assemblyName.Name + ".dll");
        }
    }
}
