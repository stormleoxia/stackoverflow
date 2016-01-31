using DynamicUsage.Benchmarks;

namespace DynamicUsage
{
    public class MyAnotherClass : MyBaseClass {
        public override int InvokeMethod()
        {
            return 2;
        }

        public override int Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}