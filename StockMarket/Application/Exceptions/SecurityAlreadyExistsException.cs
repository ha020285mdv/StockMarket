namespace StockMarket.Application.Exceptions;

public class SecurityAlreadyExistsException : Exception
{
    public SecurityAlreadyExistsException(string ticker)
        : base($"Person with personal identifier {ticker} already exists.")
    {
    }
}