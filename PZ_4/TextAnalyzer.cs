using System;
using System.Collections.Generic;
using System.Linq;

namespace PZ_4
{
    /// <summary>
    /// Класс для анализа текста
    /// </summary>
    public class TextAnalyzer
    {
        private static readonly char[] _wordSeparators = new[] { ' ', '\t', '\n', '\r' };

        /// <summary>
        /// Анализирует текст и возвращает статистику
        /// </summary>
        public TextStatistics Analyze(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new TextStatistics();
            }

            return new TextStatistics
            {
                WordCount = CountWords(text),
                LongestWord = FindLongestWord(text),
                CharacterFrequency = CalculateCharacterFrequency(text)
            };
        }

        private int CountWords(string text)
        {
            return text.Split(_wordSeparators, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        private string FindLongestWord(string text)
        {
            var words = text.Split(_wordSeparators, StringSplitOptions.RemoveEmptyEntries);
            return words.OrderByDescending(word => word.Length).FirstOrDefault() ?? string.Empty;
        }

        private Dictionary<char, int> CalculateCharacterFrequency(string text)
        {
            return text.Where(char.IsLetterOrDigit)
                      .Select(char.ToLower)
                      .GroupBy(c => c)
                      .ToDictionary(group => group.Key, group => group.Count());
        }

        /// <summary>
        /// Отображает результаты анализа в консоли
        /// </summary>
        public void DisplayResults(TextStatistics statistics)
        {
            if (statistics == null)
            {
                Console.WriteLine("Статистика недоступна.");
                return;
            }

            Console.WriteLine($"Количество слов: {statistics.WordCount}");
            Console.WriteLine($"Самое длинное слово: {statistics.LongestWord}");
            Console.WriteLine("Частота символов:");

            foreach (var pair in statistics.CharacterFrequency.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }
    }
}