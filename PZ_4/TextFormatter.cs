using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PZ_4
{
    /// <summary>
    /// Класс для форматирования текста
    /// </summary>
    public class TextFormatter
    {
        public const int DefaultLineWidth = 80;

        /// <summary>
        /// Форматирует текст согласно указанным опциям
        /// </summary>
        public string FormatText(string input, FormatOptions options)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string result = input;

            if (options.RemoveExtraSpaces)
            {
                result = RemoveExtraSpaces(result);
            }

            result = ApplyCaseFormatting(result, options.ConvertToUpper);

            if (options.JustifyText)
            {
                result = JustifyText(result, options.LineWidth);
            }

            return result;
        }

        private string RemoveExtraSpaces(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            StringBuilder sb = new StringBuilder();
            bool lastWasSpace = false;

            foreach (char c in text)
            {
                if (c == ' ' || c == '\t')
                {
                    if (!lastWasSpace)
                    {
                        sb.Append(' ');
                        lastWasSpace = true;
                    }
                }
                else
                {
                    sb.Append(c);
                    lastWasSpace = false;
                }
            }

            return sb.ToString().Trim();
        }

        private string ApplyCaseFormatting(string text, bool toUpper)
        {
            return toUpper ? text.ToUpper() : text.ToLower();
        }

        private string JustifyText(string text, int lineWidth)
        {
            if (string.IsNullOrEmpty(text) || lineWidth <= 0)
                return text;

            string[] words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
                return text;

            List<string> lines = new List<string>();
            List<string> currentLine = new List<string>();
            int currentLength = 0;

            foreach (string word in words)
            {
                if (currentLength + word.Length + currentLine.Count > lineWidth && currentLine.Count > 0)
                {
                    lines.Add(CreateJustifiedLine(currentLine, lineWidth));
                    currentLine.Clear();
                    currentLength = 0;
                }

                currentLine.Add(word);
                currentLength += word.Length;
            }

            if (currentLine.Count > 0)
            {
                lines.Add(string.Join(" ", currentLine));
            }

            return string.Join("\n", lines);
        }

        private string CreateJustifiedLine(List<string> words, int width)
        {
            if (words.Count == 0)
                return string.Empty;

            if (words.Count == 1)
                return words[0];

            int totalSpaces = width - words.Sum(w => w.Length);
            int baseSpaces = totalSpaces / (words.Count - 1);
            int extraSpaces = totalSpaces % (words.Count - 1);

            StringBuilder line = new StringBuilder();

            for (int i = 0; i < words.Count; i++)
            {
                line.Append(words[i]);

                if (i < words.Count - 1)
                {
                    int spacesToAdd = baseSpaces + (i < extraSpaces ? 1 : 0);
                    line.Append(' ', spacesToAdd);
                }
            }

            return line.ToString();
        }
    }

    /// <summary>
    /// Класс для настройки параметров форматирования
    /// </summary>
    public class FormatOptions
    {
        public bool RemoveExtraSpaces { get; set; }
        public bool ConvertToUpper { get; set; }
        public bool JustifyText { get; set; }
        public int LineWidth { get; set; } = TextFormatter.DefaultLineWidth;
    }
}