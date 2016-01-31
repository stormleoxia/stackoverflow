using System.Collections.Generic;
using NUnit.Framework;

namespace DynamicUsage.Benchmarks
{
    internal sealed class ClassCall : IBenchmark
    {
        private readonly MyClass _instanceOne;
        private readonly MyAnotherClass _instanceTwo;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ClassCall()
        {
            _instanceOne = new MyClass();
            _instanceTwo = new MyAnotherClass();
        }

                public string Name {
            get { return "Class standard Call"; }
        }

        public void Benchmark()
        {
            _instanceOne.InvokeMethod();
            _instanceTwo.InvokeMethod();
        }

        public void VerifyAssertions()
        {
            List<int> list = new List<int>();
            list.Add(_instanceOne.InvokeMethod());
            list.Add(_instanceTwo.InvokeMethod());
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
        }
    }
}
