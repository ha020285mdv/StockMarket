namespace StockMarket.Application.Exceptions;

public class SecurityNotFoundException : Exception
{
    public SecurityNotFoundException(string ticker)
        : base($"Person with personal identifier {ticker} was not found.")
    {
    }
}