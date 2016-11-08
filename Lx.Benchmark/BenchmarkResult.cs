namespace Lx.Benchmark
{
    public class BenchmarkResult
    {
        public string Name { get; private set; }
        public long ElapsedMilliseconds { get; private set; }

        public BenchmarkResult(string name, long elapsedMilliseconds)
        {
            Name = name;
            ElapsedMilliseconds = elapsedMilliseconds;
        }
    }
}