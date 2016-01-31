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

        private static readonly List<BenchmarkResult> _results = new List<BenchmarkResult>();

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
            foreach (var benchmark in _benchmarks)
            {
                Console.WriteLine("For " + benchmark.Name + ":");
                Benchmark(benchmark);
            }
            Console.WriteLine(" == SUMMARY ==");
            _results.Sort(new ResultComparer());
            // Let's take the first item as the benchmark
            var reference = _results[0];
            Console.WriteLine(reference.Name + ": 1");
            for (int index = 1; index < _results.Count; index++)
            {
                var result = _results[index];
                Console.WriteLine("{0} : {1:0.00} ", result.Name,  ((double)result.ElapsedMilliseconds) / reference.ElapsedMilliseconds);
            }
            Console.ReadLine();
        }

        private static void Benchmark(IBenchmark benchmark)
        {
            benchmark.VerifyAssertions();
            benchmark.Benchmark(); // First run outside of timing to avoid any warm up side effect
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

    internal class ResultComparer : IComparer<BenchmarkResult>
    {
        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <returns>
        /// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>, as shown in the following table.Value Meaning Less than zero<paramref name="x"/> is less than <paramref name="y"/>.Zero<paramref name="x"/> equals <paramref name="y"/>.Greater than zero<paramref name="x"/> is greater than <paramref name="y"/>.
        /// </returns>
        /// <param name="x">The first object to compare.</param><param name="y">The second object to compare.</param>
        public int Compare(BenchmarkResult x, BenchmarkResult y)
        {
            return (int)(x.ElapsedMilliseconds - y.ElapsedMilliseconds);
        }
    }
}
