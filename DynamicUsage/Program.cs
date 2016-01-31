using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicUsage.Benchmarks;

namespace DynamicUsage
{
    internal class Program
    {

        private static List<BenchmarkResult> _results = new List<BenchmarkResult>();

        private static readonly IBenchmark[] _benchmarks = new IBenchmark[]
        {
            new ClassCall(), 
            new DelegateCall(), 
            new DynamicCall(),
            new ExpressionCall(), 
            new ExtensionMethodCall(), 
            new GenericMethodCall(),
            new InterfaceCall(),
            new MethodInfoCall(), 
            new VisitorCall(),
        };

        private static void Main(string[] args)
        {
            foreach (var benchmark in _benchmarks)
            {
                Console.WriteLine("For " + benchmark.Name + ":");
                Benchmark(benchmark);
            }
            Console.ReadLine();
        }

        private static void Benchmark(IBenchmark benchmark)
        {
            benchmark.VerifyAssertions();
            benchmark.Benchmark(); // First run outside of timing
            GC.Collect(); // Collect before run to avoid any GC effects during run
            var iterations = 10000000;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < iterations; i++)
            {
                benchmark.Benchmark();
            }
            watch.Stop();
            Console.WriteLine("Elapsed: {0}", watch.Elapsed);
            Console.WriteLine("Call/ms: {0}",  iterations / watch.ElapsedMilliseconds);
            _results.Add(new BenchmarkResult(benchmark.Name, watch.ElapsedMilliseconds));
        }
    }
}
