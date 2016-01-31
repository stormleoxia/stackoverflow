using DynamicUsage.Benchmarks;

namespace DynamicUsage
{
    public abstract class MyBaseClass : IInvoker
    {
        public abstract int InvokeMethod();
        public abstract int  Accept(IVisitor visitor);
    }
}