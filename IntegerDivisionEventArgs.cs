namespace Lab4;

public class IntegerDivisionEventArgs : EventArgs
{
    public int Dividend { get; }
    public int Divisor { get; }
    public int Quotient { get; }
    public int Remainder { get; }

    public bool HasRemainder => Remainder != 0;

    public IntegerDivisionEventArgs(int dividend, int divisor, int quotient, int remainder)
    {
        Dividend = dividend;
        Divisor = divisor;
        Quotient = quotient;
        Remainder = remainder;
    }
}
