using System;
using Lx.Benchmark;
using NUnit.Framework;

namespace SplitString.Benchmarks
{
    [TestFixture]
    public sealed class StringSplitLines : IBenchmark
    {
        private string[] _result;

        public void Benchmark()
        {
            var input = StringGenerator.GenerateString();
            _result = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);            
        }

        [Test]
        public void VerifyAssertions()
        {
            var input = StringGenerator.GenerateString();
            Assert.IsNotNullOrEmpty(input);
            Assert.True(input.Contains("\r"));
            Assert.True(input.Contains("\r\n"));
            Assert.True(input.Contains("\n"));
            Benchmark();
            Assert.IsNotNull(_result);
            Assert.AreEqual(10, _result.Length);
        }

        public string Name => "String.Split";
    }
}
