using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DynamicMethodEmitter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dynamicAssembly = new DynamicAssembly("MyDynamicAsm");
            var method = dynamicAssembly.BuildMethod("MyMethod", typeof(DelegateAdd));
            dynamicAssembly.Save();
            var res = (int)method.DynamicInvoke((DelegateAdd)Add, 1, 2);
            Console.WriteLine(res);
            Console.ReadLine();
        }

        public static int EquivalentToDynamicMethod(DelegateAdd method, int parameter1, int parameter2)
        {
            return method(parameter1, parameter2);
        }

        public delegate int DelegateAdd(int parameter1, int parameter2);

        public static int Add(int parameter1, int parameter2)
        {
            return parameter1 + parameter2;
        }
    }


}
