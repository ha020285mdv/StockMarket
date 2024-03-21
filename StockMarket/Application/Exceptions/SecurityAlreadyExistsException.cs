namespace StockMarket.Application.Exceptions;

public class SecurityAlreadyExistsException : Exception
{
    public SecurityAlreadyExistsException(string ticker)
        : base($"Security with tiker '{ticker}' already exists.")
    {
    }
}