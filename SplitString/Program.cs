using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lx.Benchmark;
using SplitString.Benchmarks;

namespace SplitString
{
    class Program
    {
        private static readonly IBenchmark[] _benchmarks = new IBenchmark[]
        {
            new StringSplitLines(),
            new RegexSplitLines(),
            new RegexCompiledSplitLines(),
            new RegexCompiledSplitPattern2Lines(),
            new RegexCompiledToAssemblySplitLines(),
        };

        private static void Main(string[] args)
        {
            var runner = new BenchmarkRunner(200000);
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
