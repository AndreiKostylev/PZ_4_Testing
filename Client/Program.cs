using System;
using PZ_4;

namespace Client
{
    internal class Program
    {
        private static TextAnalyzer _analyzer = new TextAnalyzer();
        private static TextFormatter _formatter = new TextFormatter();

        static void Main(string[] args)
        {
            Console.WriteLine("=== Текстовая обработка ===");

            bool running = true;
            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AnalyzeText();
                        break;
                    case "2":
                        FormatText();
                        break;
                    case "3":
                        running = false;
                        Console.WriteLine("Выход из программы...");
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор! Попробуйте снова.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Анализ текста");
            Console.WriteLine("2. Форматирование текста");
            Console.WriteLine("3. Выход");
            Console.Write("Ваш выбор: ");
        }

        static void AnalyzeText()
        {
            Console.Write("Введите текст для анализа: ");
            string text = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Текст не может быть пустым!");
                return;
            }

            var statistics = _analyzer.Analyze(text);
            _analyzer.DisplayResults(statistics);
        }

        static void FormatText()
        {
            Console.Write("Введите текст для форматирования: ");
            string text = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Текст не может быть пустым!");
                return;
            }

            var options = new FormatOptions();

            Console.Write("Удалить лишние пробелы? (y/n): ");
            options.RemoveExtraSpaces = Console.ReadLine().ToLower() == "y";

            Console.Write("В верхний регистр? (y/n): ");
            options.ConvertToUpper = Console.ReadLine().ToLower() == "y";

            Console.Write("Выровнять текст по ширине? (y/n): ");
            options.JustifyText = Console.ReadLine().ToLower() == "y";

            if (options.JustifyText)
            {
                Console.Write($"Ширина линии (по умолчанию {TextFormatter.DefaultLineWidth}): ");
                string widthInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(widthInput))
                {
                    if (int.TryParse(widthInput, out int customWidth) && customWidth > 0)
                    {
                        options.LineWidth = customWidth;
                    }
                    else
                    {
                        Console.WriteLine($"Некорректная ширина. Используется значение по умолчанию: {TextFormatter.DefaultLineWidth}");
                        options.LineWidth = TextFormatter.DefaultLineWidth;
                    }
                }
            }

            string formattedText = _formatter.FormatText(text, options);
            Console.WriteLine("\nФорматированный текст:");
            Console.WriteLine("======================");
            Console.WriteLine(formattedText);
            Console.WriteLine("======================");
        }
    }
}