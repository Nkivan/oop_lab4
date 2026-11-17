namespace Lab4;

public static class CalculatorDemo
{
    public static void Run()
    {
        Console.WriteLine("################ ЗАВДАННЯ 2-3: подія цілочисельного ділення ################");
        Console.WriteLine();

        Calculator first = new Calculator("Калькулятор-1", 100);
        Calculator second = new Calculator("Калькулятор-2", 7);
        DivisionConsoleHandler console = new DivisionConsoleHandler();
        DivisionStatisticsHandler statistics = new DivisionStatisticsHandler();

        first.IntegerDivision += console.HandleIntegerDivision;
        first.IntegerDivision += statistics.HandleIntegerDivision;
        second.IntegerDivision += console.HandleIntegerDivision;

        Console.WriteLine($"{first.Name}: початкове значення {first.Result}");
        first.Divide(7);
        first.Add(6);
        Console.WriteLine($"   після додавання 6: {first.Result}");
        first.Divide(4);

        Console.WriteLine();
        Console.WriteLine($"{second.Name}: початкове значення {second.Result}");
        second.Multiply(3);
        Console.WriteLine($"   після множення на 3: {second.Result}");
        second.Divide(4);

        Console.WriteLine();
        Console.WriteLine("Консольний обробник відписано від першого калькулятора (-=). Тепер повідомлень немає:");
        first.IntegerDivision -= console.HandleIntegerDivision;
        first.Divide(2);
        Console.WriteLine($"   {first.Name}: результат {first.Result}, статистика досі веде облік");
        Console.WriteLine($"   {statistics.Summary()}");

        Console.WriteLine();
        Console.WriteLine("Некоректні операції:");
        try { second.Divide(0); }
        catch (DivideByZeroException ex) { Console.WriteLine($"   {ex.Message}"); }

        second.Reset(int.MaxValue);
        try { second.Add(1); }
        catch (OverflowException) { Console.WriteLine("   Переповнення розрядної сітки при додаванні."); }
        Console.WriteLine();
    }
}
