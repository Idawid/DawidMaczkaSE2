using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE2_UnitTest
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
            {
                return 0;
            }

            char[] delimiters = GetDelimiters(numbers, out numbers);

            var splitNumbers = numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(int.Parse)
                                        .ToList();

            if (splitNumbers.Any(n => n < 0))
            {
                var negativeNumbers = splitNumbers.Where(n => n < 0).ToList();
                throw new ArgumentException($"negatives not allowed: {string.Join(",", negativeNumbers)}");
            }

            return splitNumbers.Where(n => n <= 1000).Sum();
        }

        private char[] GetDelimiters(string numbers, out string newNumbers)
        {
            char[] delimiters = new char[] { ',', '\n' };

            if (numbers.StartsWith("//"))
            {
                int delimiterEndIndex = numbers.IndexOf("\n");
                string delimiterString = numbers.Substring(2, delimiterEndIndex - 2);

                if (delimiterString.StartsWith("[") && delimiterString.EndsWith("]"))
                {
                    // handle multi-char delimiter
                    delimiters = delimiterString.Substring(1, delimiterString.Length - 2).Split("][")
                                                .Select(d => d.ToCharArray())
                                                .Aggregate((d1, d2) => d1.Concat(d2).ToArray());
                }
                else
                {
                    // handle single-char delimiter
                    delimiters = new char[] { delimiterString[0] };
                }

                newNumbers = numbers.Substring(delimiterEndIndex + 1);
            }
            else
            {
                newNumbers = numbers;
            }

            return delimiters;
        }
    }
}
