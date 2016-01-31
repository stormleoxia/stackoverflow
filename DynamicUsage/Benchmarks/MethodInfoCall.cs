using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace DynamicUsage.Benchmarks
{
    [TestFixture]
    public sealed class MethodInfoCall : IBenchmark
    {
        private readonly MethodInfoCaller<MyClass> _instanceOneCaller;
        private readonly MyAnotherClass _instanceTwo;
        private readonly MyClass _instanceOne;
        private readonly MethodInfoCaller<MyAnotherClass> _instanceTwoCaller;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public MethodInfoCall()
        {
            _instanceOne = new MyClass();
            _instanceTwo = new MyAnotherClass();
            _instanceOneCaller = new MethodInfoCaller<MyClass>();
            _instanceTwoCaller = new MethodInfoCaller<MyAnotherClass>();
        }

                public string Name {
            get { return "MethodInfo Invoke call"; }
        }

        public void Benchmark()
        {
            _instanceOneCaller.InvokeMethod(_instanceOne);
            _instanceTwoCaller.InvokeMethod(_instanceTwo);
        }

        [Test]
        public void VerifyAssertions()
        {
            List<int> list = new List<int>();
            list.Add(_instanceOneCaller.InvokeMethod(_instanceOne));
            list.Add(_instanceTwoCaller.InvokeMethod(_instanceTwo));
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
        }
    }

    internal class MethodInfoCaller<T>
    {
        private readonly MethodInfo _method;
        private static readonly object[] _emptyParameters = new object[0];

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public MethodInfoCaller()
        {
            _method = typeof (T).GetMethod("InvokeMethod");
        }

        public int InvokeMethod(T instance)
        {
            return (int)_method.Invoke(instance, _emptyParameters);
        }
    }
}
