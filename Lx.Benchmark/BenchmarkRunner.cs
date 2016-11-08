using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lx.Benchmark
{
    public class BenchmarkRunner
    {
        private readonly List<BenchmarkResult> _results = new List<BenchmarkResult>();
        private readonly int iterations;

        public BenchmarkRunner(int iterations)
        {
            this.iterations = iterations;
        }

        public void Run(IBenchmark benchmark)
        {
            Console.WriteLine("For " + benchmark.Name + ":");
            benchmark.VerifyAssertions();
            benchmark.Benchmark(); // First run outside of timing to avoid any warm up side effect
            GC.Collect(); // Collect before run to avoid any GC effects during run
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < iterations; i++)
            {
                benchmark.Benchmark();
            }
            watch.Stop();
            Console.WriteLine("Elapsed: {0}", watch.Elapsed);
            Console.WriteLine("Call/ms: {0}", iterations / watch.ElapsedMilliseconds);
            _results.Add(new BenchmarkResult(benchmark.Name, watch.ElapsedMilliseconds));
        }

        public void DisplayResults()
        {
            Console.WriteLine(" == SUMMARY ==");
            _results.Sort(new ResultComparer());
            // Let's take the first item as the benchmark
            var reference = _results[0];
            Console.WriteLine(reference.Name + ": 1");
            for (int index = 1; index < _results.Count; index++)
            {
                var result = _results[index];
                Console.WriteLine("{0} : {1:0.00} ", result.Name,
                    ((double)result.ElapsedMilliseconds) / reference.ElapsedMilliseconds);
            }
        }
    }
}