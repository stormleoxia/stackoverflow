﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicUsage.Benchmarks;
using Lx.Benchmark;

namespace DynamicUsage
{
    internal class Program
    {

        private static readonly IBenchmark[] _benchmarks = new IBenchmark[]
        {
            new ClassCall(), 
            new DelegateCall(), 
            new DynamicCall(),
            new DynamicMethodCall(), 
            new ExpressionCall(), 
            new ExtensionMethodCall(), 
            new GenericMethodCall(),
            new InterfaceCall(),
            new MethodInfoCall(), 
            new VisitorCall(),
        };

        private static void Main(string[] args)
        {
            var runner = new BenchmarkRunner(10000000);
            foreach (var benchmark in _benchmarks)
            {
                runner.Run(benchmark);
            }
            runner.DisplayResults();
            if (Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }


    }
}
