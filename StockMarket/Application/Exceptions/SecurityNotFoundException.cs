namespace StockMarket.Application.Exceptions;

public class SecurityNotFoundException : Exception
{
    public SecurityNotFoundException(string ticker)
        : base($"Security with ticker '{ticker}' was not found.")
    {
    }
}