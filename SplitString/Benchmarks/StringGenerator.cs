using System;
using System.Text;

namespace SplitString.Benchmarks
{
    public class StringGenerator
    {
        private static readonly Random _random = new Random(Environment.TickCount); 

        /// <summary>
        /// Generates a new string to avoid any caching bias.
        /// </summary>
        /// <returns></returns>
        public static string GenerateString()
        {
            var builder = new StringBuilder();
            builder.Append("\n");
            for (int i = 0; i < 6; ++i)
            {
                builder.Append(_random.Next());
                builder.Append(GetLineSeparator(i));
            }
            // add empty line for each separator
            builder.Append("\r\n");
            builder.Append("\r");
            return builder.ToString();
        }

        public static string GetLineSeparator(int index)
        {
            switch (index%3)
            {
                case 0:
                {
                    return "\r";
                }
                case 1:
                {
                    return "\n";
                }
                case 2:
                {
                    return "\r\n";
                }
                default:
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
    }
}