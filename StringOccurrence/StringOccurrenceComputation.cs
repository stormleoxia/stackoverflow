#region Copyright (c) Leoxia Ltd All Rights Reserved

// 
// <copyright file="Md5VsSha256.cs" company="Leoxia Ltd">
//     Copyright © 2011-2022 Leoxia Ltd. All Rights Reserved
// </copyright>
// 
// .NET Software Development
// https://www.leoxia.com
// Build. Tomorrow. Together
// 
// Any redistribution or reproduction of part or all the software (either in code or in binary form)
// is strictly prohibited without Leoxia prior written permission.

#endregion

using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using Microsoft.VisualBasic;

namespace StringOccurrence;

// some text from Shakespeare Richard III

public class ShortInShortComputation
{
    private readonly LukeH _lukeH;
    private readonly RichardWatson _richard;
    private readonly Fab _fab;

    public ShortInShortComputation()
    {
        _lukeH = new LukeH();
        _richard = new RichardWatson();
        _fab = new Fab();
    }

    [Benchmark(Baseline = true)]
    public int Richard_ShortInShort() => _richard.GetOccurrences(Constants.ShortText, Constants.ShortQuery);

    [Benchmark]
    public int Luke_ShortInShort() => _lukeH.GetOccurrences(Constants.ShortText, Constants.ShortQuery);

    [Benchmark]
    public int Fab_ShortInShort() => _fab.GetOccurrences(Constants.ShortText, Constants.ShortQuery);
}

public class ShortInLongComputation
{
    private readonly LukeH _lukeH;
    private readonly RichardWatson _richard;
    private readonly Fab _fab;

    public ShortInLongComputation()
    {
        _lukeH = new LukeH();
        _richard = new RichardWatson();
        _fab = new Fab();
    }

    [Benchmark(Baseline = true)]
    public int Richard_ShortInLong() => _richard.GetOccurrences(Constants.HugeText, Constants.ShortQuery);

    [Benchmark]
    public int Luke_ShortInLong() => _lukeH.GetOccurrences(Constants.HugeText, Constants.ShortQuery);

    [Benchmark]
    public int Fab_ShortInLong() => _fab.GetOccurrences(Constants.HugeText, Constants.ShortQuery);
}

public class LongInLongComputation
{
    private readonly LukeH _lukeH;
    private readonly RichardWatson _richard;
    private readonly Fab _fab;

    public LongInLongComputation()
    {
        _lukeH = new LukeH();
        _richard = new RichardWatson();
        _fab = new Fab();
    }

    [Benchmark(Baseline = true)]
    public int Richard_LongInLong() => _richard.GetOccurrences(Constants.HugeText, Constants.LongQuery);

    [Benchmark]
    public int Luke_LongInLong() => _lukeH.GetOccurrences(Constants.HugeText, Constants.LongQuery);

    [Benchmark]
    public int Fab_LongInLong() => _fab.GetOccurrences(Constants.HugeText, Constants.LongQuery);
}

public class LukeH : IStringOccurrenceProvider
{
    public int GetOccurrences(string input, string needle)
    {
        return input.Split(needle).Length - 1;
    }
}

public class Fab : IStringOccurrenceProvider
{
public int GetOccurrences(string input, string needle)
{
    int count = 0;
    unchecked
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(needle))
        {
            return 0;
        }

        for (var i = 0; i < input.Length - needle.Length + 1; i++)
        {
            var c = input[i];
            if (c == needle[0])
            {
                for (var index = 0; index < needle.Length; index++)
                {
                    c = input[i + index];
                    var n = needle[index];

                    if (c != n)
                    {
                        break;
                    }
                    else if (index == needle.Length - 1)
                    {
                        count++;
                    }
                }
            }
        }
    }

    return count;
}
}

public class RichardWatson : IStringOccurrenceProvider
{
    public int GetOccurrences(string input, string needle)
    {
        int count = 0;
        int n = 0;

        while ((n = input.IndexOf(needle, n)) != -1)
        {
            n++;
            count++;
        }

        return count;
    }
}

public interface IStringOccurrenceProvider
{
    int GetOccurrences(string input, string needle);
}