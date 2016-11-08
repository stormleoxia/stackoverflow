namespace Lx.Benchmark
{
    public interface IBenchmark
    {
        void Benchmark();
        void VerifyAssertions();
        string Name { get; }
    }
}