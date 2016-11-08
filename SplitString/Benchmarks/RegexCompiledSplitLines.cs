using System;
using System.Text.RegularExpressions;
using Lx.Benchmark;
using NUnit.Framework;

namespace SplitString.Benchmarks
{
    [TestFixture]
    public sealed class RegexCompiledSplitLines : IBenchmark
    {
        private string[] _result;
        private readonly Regex _regex;

        public RegexCompiledSplitLines()
        {
            _regex = new Regex("\r\n|\r|\n", RegexOptions.Multiline | RegexOptions.Compiled);           
        }

        public void Benchmark()
        {
            var input = StringGenerator.GenerateString();
            _result = _regex.Split(input);
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
    

        public string Name => "(Compiled)regex.Split(\"\\r\\n|\\r|\\n\")";
    }

    [TestFixture]
    public sealed class RegexCompiledSplitPattern2Lines : IBenchmark
    {
        private string[] _result;
        private readonly Regex _regex;

        public RegexCompiledSplitPattern2Lines()
        {
            _regex = new Regex("\r?\n|\r", RegexOptions.Multiline | RegexOptions.Compiled);
        }

        public void Benchmark()
        {
            var input = StringGenerator.GenerateString();
            _result = _regex.Split(input);
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


        public string Name => "(Compiled)regex.Split(\"\\r?\\n|\\r\")";
    }
}