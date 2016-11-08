using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Lx.Benchmark;
using NUnit.Framework;

namespace SplitString.Benchmarks
{

    /// <summary>
    /// From https://msdn.microsoft.com/en-us/library/gg578045(v=vs.110).aspx
    /// </summary>
    /// <seealso cref="Lx.Benchmark.IBenchmark" />
    [TestFixture]
    public sealed class RegexCompiledToAssemblySplitLines : IBenchmark
    {
        private string[] _result;
        private readonly Regex _regex;

        public RegexCompiledToAssemblySplitLines()
        {
            RegexCompilationInfo SentencePattern =
                           new RegexCompilationInfo("\r\n|\r|\n",
                                                    RegexOptions.Multiline,
                                                    "SentencePattern",
                                                    "Utilities.RegularExpressions",
                                                    true);
            RegexCompilationInfo[] regexes = { SentencePattern };
            AssemblyName assemName = new AssemblyName("RegexLib, Version=1.0.0.1001, Culture=neutral, PublicKeyToken=null");
            Regex.CompileToAssembly(regexes, assemName);
            var asm = Assembly.Load(assemName);
            var type = asm.GetType("Utilities.RegularExpressions.SentencePattern");
            _regex = (Regex) Activator.CreateInstance(type);
        }

        public void Benchmark()
        {
            var input = StringGenerator.GenerateString();
            _result = _regex.Split(input);
        }

        [Test]
        public void VerifyAssertions()
        {
            Assert.IsNotNull(_regex);
            var input = StringGenerator.GenerateString();
            Assert.IsNotNullOrEmpty(input);
            Assert.True(input.Contains("\r"));
            Assert.True(input.Contains("\r\n"));
            Assert.True(input.Contains("\n"));
            Benchmark();
            Assert.IsNotNull(_result);
            Assert.AreEqual(10, _result.Length);
        }


        public string Name => "(CompiledToAssembly)regex.Split()";
    }
}