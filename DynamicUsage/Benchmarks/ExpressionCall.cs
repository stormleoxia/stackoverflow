using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;

namespace DynamicUsage.Benchmarks
{
    public class ExpressionCall : IBenchmark
    {
        private readonly MyAnotherClass _instanceTwo;
        private readonly MyClass _instanceOne;
        private readonly ExpressionCaller<MyClass> _instanceOneCaller;
        private readonly ExpressionCaller<MyAnotherClass> _instanceTwoCaller;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ExpressionCall()
        {
            _instanceOne = new MyClass();
            _instanceTwo = new MyAnotherClass();
            _instanceOneCaller = new ExpressionCaller<MyClass>();
            _instanceTwoCaller = new ExpressionCaller<MyAnotherClass>();
        }

        public void Benchmark()
        {
            _instanceOneCaller.InvokeMethod(_instanceOne);
            _instanceTwoCaller.InvokeMethod(_instanceTwo);
        }

                public string Name {
            get { return "Linq Expression Call"; }
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

    internal class ExpressionCaller<T>
    {
        private readonly MethodInfo _method;
        private readonly Delegate _delegate; 
        private readonly Func<T, int> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ExpressionCaller()
        {
            _method = typeof (T).GetMethod("InvokeMethod");
            var instanceParameter = Expression.Parameter(typeof (T), "instance");
            var call = Expression.Call(instanceParameter, _method);
            _delegate = Expression.Lambda<Func<T, int>>(call, instanceParameter).Compile();
            _func = (Func<T, int>) _delegate;
        }

        public int InvokeMethod(T instance)
        {
            return _func.Invoke(instance);
        }

        public int DynamicInvokeMethod(T instance)
        {
            return (int)_delegate.DynamicInvoke(instance);
        }
    }
}
