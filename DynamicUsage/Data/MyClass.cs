using System;
using DynamicUsage.Benchmarks;

namespace DynamicUsage
{
    /// <summary>
    /// 
    /// </summary>
    public class MyClass : MyBaseClass {
        public override int InvokeMethod()
        {
            return 1;
        }

        public override int Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}