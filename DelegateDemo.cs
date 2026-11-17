namespace Lab4;

/// <summary>
/// Завдання 1: один і той самий метод (підрахунок символу в рядку), записаний двома способами -
/// анонімним методом і лямбда-виразом, та викликаний через делегат CharCountOperation.
/// Логіка обчислення записана безпосередньо в анонімному методі та в лямбда-виразі.
/// </summary>
public static class DelegateDemo
{
    /// <summary>Анонімний метод (ключове слово delegate, явні типи параметрів, цикл for).</summary>
    public static readonly CharCountOperation AnonymousMethod = delegate (string text, char symbol)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));

        int count = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == symbol) count++;
        }
        return count;
    };

    /// <summary>Лямбда-вираз (типи параметрів виводяться з делегата, підрахунок через LINQ).</summary>
    public static readonly CharCountOperation Lambda = (text, symbol) =>
        text == null
            ? throw new ArgumentNullException(nameof(text))
            : text.Count(c => c == symbol);

    public static void Run()
    {
        Console.WriteLine("################ ЗАВДАННЯ 1: делегат, анонімний метод, лямбда-вираз ################");
        Console.WriteLine();

        (string Text, char Symbol)[] samples =
        {
            ("banana", 'a'),
            ("Hello, World!", 'l'),
            ("Об'єктно-орієнтоване програмування", 'о'),
            ("", 'x')
        };

        foreach ((string text, char symbol) in samples)
        {
            // Виклик через делегат
            int viaAnonymous = AnonymousMethod(text, symbol);
            int viaLambda = Lambda(text, symbol);

            Console.WriteLine($"Рядок \"{text}\", символ '{symbol}':");
            Console.WriteLine($"   анонімний метод: {viaAnonymous}, лямбда-вираз: {viaLambda}, " +
                              $"результати збігаються: {viaAnonymous == viaLambda}");
        }

        ShowDelegateInternals();
        ShowClosureSideEffect();
        Console.WriteLine();
    }

    private static void ShowDelegateInternals()
    {
        Console.WriteLine();
        Console.WriteLine("--- Що таке делегат всередині ---");
        Console.WriteLine($"Тип змінної: {AnonymousMethod.GetType().Name} (похідний від MulticastDelegate: " +
                          $"{AnonymousMethod is MulticastDelegate})");
        Console.WriteLine($"Список викликів: {AnonymousMethod.GetInvocationList().Length} метод(и)");
    }

    /// <summary>
    /// Побічний ефект замикання: лямбда захоплює змінну (а не її значення), тому результат
    /// залежить від значення змінної в момент виклику.
    /// </summary>
    private static void ShowClosureSideEffect()
    {
        Console.WriteLine();
        Console.WriteLine("--- Побічний ефект замикання ---");

        char target = 'a';
        Func<string, int> countTarget = text => text.Count(c => c == target);   // захоплює змінну target

        Console.WriteLine($"target = '{target}': в \"banana\" знайдено {countTarget("banana")}");
        target = 'n';   // змінили змінну ПІСЛЯ створення лямбди
        Console.WriteLine($"target = '{target}': в \"banana\" знайдено {countTarget("banana")} (той самий вираз, інший результат)");
    }
}
