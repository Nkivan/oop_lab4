namespace Lab4;


public class DivisionConsoleHandler
{
    public void HandleIntegerDivision(object sender, IntegerDivisionEventArgs e)
    {
        string initiator = sender is Calculator calculator ? calculator.Name : "невідомий об'єкт";
        string remainder = e.HasRemainder ? $", остача {e.Remainder}" : ", без остачі";

        Console.WriteLine($"   [Подія] {initiator}: цілочисельне ділення {e.Dividend} / {e.Divisor} = {e.Quotient}{remainder}");
    }
}
