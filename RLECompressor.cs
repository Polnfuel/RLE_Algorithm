using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace RLE_Algorithm
{
    public class RLECompressor : TextCompressor
    {
        public override string CompressText(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException();
            if (Regex.IsMatch(input, @"\d"))
                throw new FormatException();
            StringBuilder result = new StringBuilder();
            int count = 1;
            for (int i = 1; i <= input.Length; i++)
            {
                if (i == input.Length || input[i] != input[i - 1])
                {
                    if (count == 1)
                        result.Append(input[i - 1]);
                    else
                        result.Append(count).Append(input[i - 1]);
                    count = 1;
                }
                else
                {
                    count++;
                }
            }
            return result.ToString();
        }
        public override string DecompressText(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException();
            if (!Regex.IsMatch(input, @"\d"))
                throw new FormatException();
            StringBuilder result = new StringBuilder();
            string number = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    number += input[i].ToString();
                }
                else
                {
                    if (number == string.Empty)
                        result.Append(input[i]);
                    else
                    {
                        int count = int.Parse(number);
                        char value = input[i];
                        result.Append(new string(value, count));
                        number = string.Empty;
                    }
                }
            }
            return result.ToString();
        }
    }
}
