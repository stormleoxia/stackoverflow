using BenchmarkDotNet.Running;

namespace StringOccurrence;

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
    }
}