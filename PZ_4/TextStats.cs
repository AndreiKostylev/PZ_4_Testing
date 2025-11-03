using System;
using System.Collections.Generic;

namespace PZ_4
{
    /// <summary>
    /// Класс для хранения статистики текста
    /// </summary>
    public class TextStatistics
    {
        public int WordCount { get; set; }
        public string LongestWord { get; set; }
        public Dictionary<char, int> CharacterFrequency { get; set; }

        public TextStatistics()
        {
            WordCount = 0;
            LongestWord = string.Empty;
            CharacterFrequency = new Dictionary<char, int>();
        }
    }
}