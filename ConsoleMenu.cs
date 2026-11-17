using System.Text;

namespace Lab4;

internal sealed class ConsoleMenu
{
    public void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        try
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== ЛАБОРАТОРНА РОБОТА 4 (варіант 2) =====");
                Console.WriteLine("1. Демонстрація: делегат, анонімний метод, лямбда-вираз");
                Console.WriteLine("2. Демонстрація: калькулятор і подія цілочисельного ділення");
                Console.WriteLine("3. Підрахунок символу в рядку (введення з клавіатури)");
                Console.WriteLine("4. Калькулятор (інтерактивно, з підпискою на подію)");
                Console.WriteLine("0. Вихід");

                switch (ReadLine("Ваш вибір: "))
                {
                    case "1": DelegateDemo.Run(); break;
                    case "2": CalculatorDemo.Run(); break;
                    case "3": CountSymbolInteractive(); break;
                    case "4": CalculatorInteractive(); break;
                    case "0": return;
                    default: Console.WriteLine("Невідома команда."); break;
                }
            }
        }
        catch (EndOfStreamException)
        {

        }
    }

 

    private static void CountSymbolInteractive()
    {
        string text = ReadLine("Введіть рядок: ");
        char symbol = ReadSymbol("Введіть символ для пошуку (один): ");

    
        int viaAnonymous = DelegateDemo.AnonymousMethod(text, symbol);
        int viaLambda = DelegateDemo.Lambda(text, symbol);

        Console.WriteLine($"Анонімний метод: символ '{symbol}' зустрічається {viaAnonymous} раз(и).");
        Console.WriteLine($"Лямбда-вираз:    символ '{symbol}' зустрічається {viaLambda} раз(и).");
    }



    private static void CalculatorInteractive()
    {
        Calculator calculator = new Calculator("Калькулятор", ReadInt("Початкове значення: "));
        DivisionConsoleHandler console = new DivisionConsoleHandler();
        DivisionStatisticsHandler statistics = new DivisionStatisticsHandler();

        bool consoleSubscribed = true;
        bool statisticsSubscribed = false;
        calculator.IntegerDivision += console.HandleIntegerDivision;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine($"Поточне значення: {calculator.Result}");
            Console.WriteLine($"Консольний обробник: {(consoleSubscribed ? "підписано" : "відписано")}, " +
                              $"лічильник: {(statisticsSubscribed ? "підписано" : "відписано")}");
            Console.WriteLine("1. Додати");
            Console.WriteLine("2. Відняти");
            Console.WriteLine("3. Помножити");
            Console.WriteLine("4. Поділити націло (генерує подію)");
            Console.WriteLine("5. Задати нове значення");
            Console.WriteLine("6. Підписатися/відписатися: консольний обробник");
            Console.WriteLine("7. Підписатися/відписатися: лічильник");
            Console.WriteLine("8. Показати статистику лічильника");
            Console.WriteLine("0. Назад");

            string choice = ReadLine("Ваш вибір: ");
            try
            {
                switch (choice)
                {
                    case "1": calculator.Add(ReadInt("Число: ")); break;
                    case "2": calculator.Subtract(ReadInt("Число: ")); break;
                    case "3": calculator.Multiply(ReadInt("Число: ")); break;
                    case "4": calculator.Divide(ReadInt("Дільник: ")); break;
                    case "5": calculator.Reset(ReadInt("Нове значення: ")); break;
                    case "6":
                        if (consoleSubscribed) calculator.IntegerDivision -= console.HandleIntegerDivision;
                        else calculator.IntegerDivision += console.HandleIntegerDivision;
                        consoleSubscribed = !consoleSubscribed;
                        break;
                    case "7":
                        if (statisticsSubscribed) calculator.IntegerDivision -= statistics.HandleIntegerDivision;
                        else calculator.IntegerDivision += statistics.HandleIntegerDivision;
                        statisticsSubscribed = !statisticsSubscribed;
                        break;
                    case "8": Console.WriteLine(statistics.Summary()); break;
                    case "0": return;
                    default: Console.WriteLine("Невідома команда."); break;
                }
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Помилка: переповнення розрядної сітки, значення не змінено.");
            }
        }
    }


    private static string ReadLine(string prompt)
    {
        Console.Write(prompt);
        string line = Console.ReadLine();
        if (line == null) throw new EndOfStreamException();
        return line;
    }

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            if (int.TryParse(ReadLine(prompt).Trim(), out int value)) return value;
            Console.WriteLine("  Введіть ціле число.");
        }
    }

    private static char ReadSymbol(string prompt)
    {
        while (true)
        {
            string text = ReadLine(prompt);
            if (text.Length == 1) return text[0];
            Console.WriteLine("  Потрібен рівно один символ.");
        }
    }
}
