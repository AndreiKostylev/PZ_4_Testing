using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ_4
{
    public class TextFormatter
    {
        public string FormatText(string input, bool removeSpaces, bool toUpper, bool justify, int width = 80)
        {
            string result = input;

            if (removeSpaces)
            {
                result = RemoveExtraSpaces(result);
            }

            if (toUpper)
            {
                result = result.ToUpper();
            }
            else
            {
                result = result.ToLower();
            }

            if (justify)
            {
                result = JustifyText(result, width);
            }

            return result;
        }

        private string RemoveExtraSpaces(string text)
        {
            StringBuilder sb = new StringBuilder();
            bool lastWasSpace = false;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
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

        private string JustifyText(string text, int lineWidth)
        {
            if (string.IsNullOrEmpty(text)) return text;

            string[] words = text.Split(' ');
            List<string> lines = new List<string>();
            List<string> currentLine = new List<string>();
            int currentLength = 0;

            foreach (string word in words)
            {
                if (currentLength + word.Length + currentLine.Count > lineWidth)
                {
                    if (currentLine.Count > 0)
                    {
                        lines.Add(CreateJustifiedLine(currentLine, lineWidth));
                        currentLine.Clear();
                        currentLength = 0;
                    }
                }

                if (word.Length > 0)
                {
                    currentLine.Add(word);
                    currentLength += word.Length;
                }
            }

            if (currentLine.Count > 0)
            {
                lines.Add(string.Join(" ", currentLine));
            }

            return string.Join("\n", lines);
        }

        private string CreateJustifiedLine(List<string> words, int width)
        {
            if (words.Count == 1) return words[0];

            int totalSpaces = width - words.Sum(w => w.Length);
            int baseSpaces = totalSpaces / (words.Count - 1);
            int extraSpaces = totalSpaces % (words.Count - 1);

            StringBuilder line = new StringBuilder();

            for (int i = 0; i < words.Count; i++)
            {
                line.Append(words[i]);

                if (i < words.Count - 1)
                {
                    line.Append(' ', baseSpaces + (i < extraSpaces ? 1 : 0));
                }
            }

            return line.ToString();
        }
    }
}
