using PZ_4;

namespace Client
{
    internal class Program
    {
        static TextAnalyzer analyzer = new TextAnalyzer();
        static TextFormatter formatter = new TextFormatter();

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
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор!");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n1. Анализ текста");
            Console.WriteLine("2. Форматирование текста");
            Console.WriteLine("3. Выход");
            Console.Write("Выбор: ");
        }

        static void AnalyzeText()
        {
            Console.Write("Введите текст: ");
            string text = Console.ReadLine();

            analyzer.ProcessAndDisplay(text);
        }

        static void FormatText()
        {
            Console.Write("Введите текст: ");
            string text = Console.ReadLine();

            Console.Write("Удалить лишние пробелы? (y/n): ");
            bool removeSpaces = Console.ReadLine().ToLower() == "y";

            Console.Write("В верхний регистр? (y/n): ");
            bool toUpper = Console.ReadLine().ToLower() == "y";

            Console.Write("Выровнять текст по ширине? (y/n): ");
            bool justify = Console.ReadLine().ToLower() == "y";

            int width = 80;
            if (justify)
            {
                Console.Write("Ширина линии (по умолчанию 80): ");
                string widthInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(widthInput))
                {
                    width = int.Parse(widthInput); 
                }
            }

            string formatted = formatter.FormatText(text, removeSpaces, toUpper, justify, width);
            Console.WriteLine("Форматированный текст:");
            Console.WriteLine(formatted);
        }
    }
}