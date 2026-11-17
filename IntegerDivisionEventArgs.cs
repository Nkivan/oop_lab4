namespace Lab4;

/// <summary>Аргумент події «цілочисельне ділення»: передає всі дані про виконане ділення.</summary>
public class IntegerDivisionEventArgs : EventArgs
{
    public int Dividend { get; }
    public int Divisor { get; }
    public int Quotient { get; }
    public int Remainder { get; }

    /// <summary>Чи було ділення з остачею.</summary>
    public bool HasRemainder => Remainder != 0;

    public IntegerDivisionEventArgs(int dividend, int divisor, int quotient, int remainder)
    {
        Dividend = dividend;
        Divisor = divisor;
        Quotient = quotient;
        Remainder = remainder;
    }
}
