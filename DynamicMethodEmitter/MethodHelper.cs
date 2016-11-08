using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMethodEmitter
{
    class MethodHelper
    {

        private static void MainForCompiled()
        {
            int res = SimilarCSharpCall("hello", null);
            Console.WriteLine(res);
            Console.WriteLine("It works!");
            Console.ReadLine();
        }



        private static int Invoke(MyClass instance, string parameter)
        {
            return instance.Invoke(parameter);
        }

        public static int SimilarCSharpCall(string parameter, string parameter2)
        {
            var result = MyFirstFunction(0);
            MySecondFunction(parameter, parameter2);
            return result;
        }

        public static int MyFirstFunction(int parameter)
        {
            return 42;
        }

        public static int MySecondFunction(string parameter, string toto)
        {
            return parameter.Length;
        }
    }
}
