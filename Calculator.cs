namespace Lab4;

/// <summary>
/// Компонент багаторазового використання (варіант 2): математичні операції над накопиченим значенням.
/// Подія IntegerDivision відповідає реальній події моделі - виконанню цілочисельного ділення.
/// Клас містить і саму подію, і метод, що її генерує.
/// </summary>
public class Calculator
{
    public string Name { get; }
    public int Result { get; private set; }

    /// <summary>
    /// Подія «цілочисельне ділення». Використано стандартний узагальнений делегат EventHandler,
    /// тому обробник має стандартний для .NET підпис (object sender, IntegerDivisionEventArgs e).
    /// </summary>
    public event EventHandler<IntegerDivisionEventArgs> IntegerDivision;

    public Calculator(string name, int initialValue = 0)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Назва не може бути порожньою.", nameof(name));
        Name = name;
        Result = initialValue;
    }

    // Арифметика виконується в checked-контексті: переповнення розрядної сітки не залишається непоміченим.
    public void Add(int value) => Result = checked(Result + value);

    public void Subtract(int value) => Result = checked(Result - value);

    public void Multiply(int value) => Result = checked(Result * value);

    public void Reset(int value = 0) => Result = value;

    /// <summary>Ціле ділення поточного значення на дільник. Після нього генерується подія IntegerDivision.</summary>
    public void Divide(int divisor)
    {
        if (divisor == 0) throw new DivideByZeroException("Ділення на нуль неможливе.");

        int dividend = Result;
        int quotient = checked(dividend / divisor);
        int remainder = checked(dividend - quotient * divisor);

        Result = quotient;
        OnIntegerDivision(new IntegerDivisionEventArgs(dividend, divisor, quotient, remainder));
    }

    /// <summary>
    /// Метод, що генерує подію. Захищений і віртуальний за прийнятою в .NET схемою:
    /// нащадки можуть змінити реакцію. Оператор ?. перевіряє, чи є підписники.
    /// </summary>
    protected virtual void OnIntegerDivision(IntegerDivisionEventArgs e)
    {
        IntegerDivision?.Invoke(this, e);
    }
}
