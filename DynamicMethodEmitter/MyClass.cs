using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMethodEmitter
{
    public class MyClass
    {
        public int Invoke(string parameter)
        {
            return parameter.Length;
        }
    }
}
