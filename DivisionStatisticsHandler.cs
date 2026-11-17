namespace Lab4;

/// <summary>Другий клас-обробник тієї самої події: веде статистику виконаних ділень.</summary>
public class DivisionStatisticsHandler
{
    public int Count { get; private set; }
    public int WithRemainder { get; private set; }
    public int LastQuotient { get; private set; }

    public void HandleIntegerDivision(object sender, IntegerDivisionEventArgs e)
    {
        Count++;
        if (e.HasRemainder) WithRemainder++;
        LastQuotient = e.Quotient;
    }

    public string Summary()
        => Count == 0
            ? "Жодного ділення ще не зафіксовано."
            : $"Усього ділень: {Count}, з остачею: {WithRemainder}, остання частка: {LastQuotient}";
}
