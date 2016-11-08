using System;
using System.Text.RegularExpressions;
using Lx.Benchmark;
using NUnit.Framework;

namespace SplitString.Benchmarks
{
    [TestFixture]
    public sealed class RegexSplitLines : IBenchmark
    {
        private string[] _result;

        public void Benchmark()
        {
            var input = StringGenerator.GenerateString();
            _result = Regex.Split(input, "\r\n|\r|\n");
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

        public string Name => "Regex.Split()";
    }
}