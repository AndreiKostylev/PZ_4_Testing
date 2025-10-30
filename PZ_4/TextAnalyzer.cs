using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ_4
{
    public class TextAnalyzer
    {
        public TextStats AnalyzeText(string text)
        {
            var stats = new TextStats();

            string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            stats.wc = words.Length;

            string longest = "";
            foreach (string word in words)
            {
                if (word.Length > longest.Length)
                {
                    longest = word;
                }
            }
            stats.lw = longest;

            Dictionary<char, int> freq = new Dictionary<char, int>();
            foreach (char c in text)
            {
                if (char.IsLetterOrDigit(c))
                {
                    char lowerC = char.ToLower(c);
                    if (freq.ContainsKey(lowerC))
                    {
                        freq[lowerC]++;
                    }
                    else
                    {
                        freq[lowerC] = 1;
                    }
                }
            }
            stats.cf = freq;

            return stats;
        }

        public void ProcessAndDisplay(string input)
        {
            var result = AnalyzeText(input);

            Console.WriteLine($"Количество слов: {result.wc}");
            Console.WriteLine($"Самое длинное слово: {result.lw}");
            Console.WriteLine("Частота символов:");

            foreach (var pair in result.cf.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }
    }
}
