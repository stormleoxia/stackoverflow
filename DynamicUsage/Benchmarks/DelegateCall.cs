using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace DynamicUsage.Benchmarks
{
    public class DelegateCall : IBenchmark
    {
        private readonly MyAnotherClass _instanceTwo;
        private readonly MyClass _instanceOne;
        private readonly DelegateCaller<MyClass> _instanceOneCaller;
        private readonly DelegateCaller<MyAnotherClass> _instanceTwoCaller;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DelegateCall()
        {
            _instanceOne = new MyClass();
            _instanceTwo = new MyAnotherClass();
            _instanceOneCaller = new DelegateCaller<MyClass>();
            _instanceTwoCaller = new DelegateCaller<MyAnotherClass>();
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

        public string Name {
            get { return "MethodInfo.CreateDelegate Call"; }
        }
    }

    internal class DelegateCaller<T>
    {
        private readonly MethodInfo _method;
        private readonly Delegate _delegate; 
        private static readonly object[] _emptyParameters = new object[0];
        private Func<T, int> _func;

        private delegate int MyDelegate(T instance);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DelegateCaller()
        {
            _method = typeof (T).GetMethod("InvokeMethod");
            _delegate = _method.CreateDelegate(typeof(Func<T, int>));
            _func = (Func<T, int>) _delegate;
        }

        public int InvokeMethod(T instance)
        {
            return _func.Invoke(instance);
        }

        public int DynamicInvokeMethod(T instance)
        {
            return (int)_delegate.DynamicInvoke(instance, _emptyParameters);
        }
    }
}
