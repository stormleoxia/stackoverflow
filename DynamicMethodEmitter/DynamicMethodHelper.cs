using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MethodContainer;

namespace DynamicMethodEmitter
{
    class DynamicMethodHelper
    {
        private delegate int CFuncDelegate(IntPtr Obj, IntPtr Arg);

        private static void MainForDynamic()
        {
            IntPtr cppFunctionPtr = Container.GetCppFunc();

            CFuncDelegate func = (CFuncDelegate)Marshal.GetDelegateForFunctionPointer(cppFunctionPtr, typeof(CFuncDelegate));
            int rc = func(Container.GetInstance(), Container.GetParameter());
            if (rc != 42)
            {
                throw new InvalidOperationException("Delegate doesn't produce the expected number : " + rc + " instead of " + 42);
            }
            Console.WriteLine(rc);
            Console.ReadLine();
            var parameter = "hello";
            var result = CreateAndInvoke(parameter);
            if (result != 42)
            {
                throw new InvalidOperationException("Delegate doesn't produce the expected number : " + result + " instead of " +
                                                    parameter.Length);
            }
            Console.WriteLine("It works!");
            Console.ReadLine();
        }

        private static int CreateAndInvoke(string parameter)
        {
            IntPtr ptr = new IntPtr();
            var debugDelegate = GetDebugDelegate(ptr, typeof(MyDelegate), typeof(MyClass));
            var instance = new MyClass();
            object[] parameters = { instance, parameter };
            var res = (int)debugDelegate.DynamicInvoke(parameters);
            //cppResult = (int)parameters[1];
            return res;
        }

        private static Delegate GetDebugDelegate(IntPtr ptr, Type delType, Type ownerType)
        {
            MethodInfo info = delType.GetMethod("Invoke");
            Type returnType = info.ReturnType;
            ParameterInfo[] parameters = info.GetParameters();
            var parameterTypesList = parameters.Select(i => i.ParameterType).ToList();
            Type[] parameterTypesPointers = parameterTypesList.Select(i => i.GetPointerType()).ToArray();

            //parameterTypesList.Add(returnType); // for out parameter

            Type[] parameterTypes = parameterTypesList.ToArray();
            var dm = new DynamicMethod($"{delType.Name}Calli", returnType, parameterTypes, ownerType, false);
            //dm.DefineParameter(parameterTypes.Length - 1, ParameterAttributes.Out, "myOutParameter");

            ILGenerator il = dm.GetILGenerator();

            //for (int i = 0; i < parameterTypesPointers.Length; i++)
            //    il.Emit(OpCodes.Ldarg, i);

            //if (IntPtr.Size == 4)
            //    il.Emit(OpCodes.Ldc_I4, ptr.ToInt32());
            //else if (IntPtr.Size == 8)
            //    il.Emit(OpCodes.Ldc_I8, ptr.ToInt64());
            //else
            //    throw new PlatformNotSupportedException();

            //il.EmitCalli(OpCodes.Calli, CallingConvention.Cdecl, returnType.GetPointerType(), parameterTypesPointers);

            //il.Emit(OpCodes.Stind_I4);

            //Create locals for out parameter
            //var local = il.DeclareLocal(returnType);
            //il.Emit(OpCodes.Ldloca, local);



            //il.Emit(OpCodes.Ldc_I4_0);
            //il.Emit(OpCodes.Ldarg_0);
            //il.Emit(OpCodes.Ldnull);
            //il.Emit(OpCodes.Stind_Ref);
            //for (int i = 0; i < parameterTypes.Length; i++)
            //    il.Emit(OpCodes.Ldarg, i);
            // If method isn't static push target instance on top of stack.

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            //il.Emit(OpCodes.Ldarg_2);
            //for (int i = 0; i < parameters.Length; i++)
            //{
            //    //Push args array reference onto the stack, followed
            //    //by the current argument index (i). The Ldelem_Ref opcode
            //    //will resolve them to args[i].
            //    // Argument 1 of dynamic method is argument array.
            //    il.Emit(OpCodes.Ldarg_1);
            //    il.Emit(OpCodes.Ldc_I4, i);

            //    // If parameter [i] is a value type perform an unboxing.
            //    Type parameterType = parameters[i].ParameterType;
            //    if (parameterType.IsValueType)
            //    {
            //        il.Emit(OpCodes.Unbox_Any, parameterType);
            //    }
            //}

            il.EmitCall(OpCodes.Call, info, null);
            //il.Emit(OpCodes.Ldc_I4_0);
            ////This does not gets called
            //Action action = () =>
            //{
            //    Console.WriteLine("test");
            //};
            //il.Emit(OpCodes.Call, action.Method);
            //il.Emit(OpCodes.Pop);
            il.Emit(OpCodes.Ret);
            return dm.CreateDelegate(delType);
        }

        public delegate int MyDelegate(MyClass instance, string parameter);


    }

}
