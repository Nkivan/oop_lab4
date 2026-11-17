namespace Lab4;


public class Calculator
{
    public string Name { get; }
    public int Result { get; private set; }

  
    public event EventHandler<IntegerDivisionEventArgs> IntegerDivision;

    public Calculator(string name, int initialValue = 0)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Назва не може бути порожньою.", nameof(name));
        Name = name;
        Result = initialValue;
    }

    public void Add(int value) => Result = checked(Result + value);

    public void Subtract(int value) => Result = checked(Result - value);

    public void Multiply(int value) => Result = checked(Result * value);

    public void Reset(int value = 0) => Result = value;

    public void Divide(int divisor)
    {
        if (divisor == 0) throw new DivideByZeroException("Ділення на нуль неможливе.");

        int dividend = Result;
        int quotient = checked(dividend / divisor);
        int remainder = checked(dividend - quotient * divisor);

        Result = quotient;
        OnIntegerDivision(new IntegerDivisionEventArgs(dividend, divisor, quotient, remainder));
    }


    protected virtual void OnIntegerDivision(IntegerDivisionEventArgs e)
    {
        IntegerDivision?.Invoke(this, e);
    }
}
