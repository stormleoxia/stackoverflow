using System.Collections.Generic;
using NUnit.Framework;

namespace DynamicUsage.Benchmarks
{
    internal class ExtensionMethodCall : IBenchmark
    {
        private readonly MyClass _instanceOne;
        private readonly MyAnotherClass _instanceTwo;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ExtensionMethodCall()
        {
            _instanceOne = new MyClass();
            _instanceTwo = new MyAnotherClass();
        }

        public void Benchmark()
        {
            _instanceOne.InvokeMethodEx();
            _instanceTwo.InvokeMethodEx();
        }

        [Test]
        public void VerifyAssertions()
        {
            List<int> list = new List<int>();
            list.Add(_instanceOne.InvokeMethodEx());
            list.Add(_instanceTwo.InvokeMethodEx());
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
        }

        public string Name {
            get { return "Extension Method Call"; }
        }    }

    public static class Invoker
    {
        public static int InvokeMethodEx(this MyAnotherClass c)
        {
            return c.InvokeMethod();
        }

        public static int InvokeMethodEx(this MyClass c)
        {
            return c.InvokeMethod();
        }

    }
}