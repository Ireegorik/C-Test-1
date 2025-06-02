using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharCounter
{
    class Program
    {

        public static string Compress(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            StringBuilder result = new StringBuilder();
            char currentChar = input[0];
            int count = 1;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == currentChar)
                {
                    count++;
                }
                else
                {
                    result.Append(currentChar);
                    if (count > 1)
                        result.Append(count);

                    currentChar = input[i];
                    count = 1;
                }
            }

            result.Append(currentChar);
            if (count > 1)
                result.Append(count);

            return result.ToString();
        }

        public static string Decompress(string compressed)
        {
            if (string.IsNullOrEmpty(compressed))
                return string.Empty;

            StringBuilder result = new StringBuilder();
            int i = 0;

            while (i < compressed.Length)
            {
                char currentChar = compressed[i++];
                result.Append(currentChar);

                int numStart = i;
                while (i < compressed.Length && char.IsDigit(compressed[i]))
                {
                    i++;
                }

                if (numStart < i)
                {
                    int count = int.Parse(compressed.Substring(numStart, i - numStart));
                    result.Append(new string(currentChar, count - 1));
                }
            }

            return result.ToString();
        }


        static void Main(string[] args)
        {
            string s = Compress("aaabbcccddee");
            Console.WriteLine(Decompress(s));
            Console.ReadKey();
        }
    }
}
