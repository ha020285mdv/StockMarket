namespace StockMarket.Application.Exceptions;

public class PortfolioNotFoundException : Exception
{
    public PortfolioNotFoundException(string email)
        : base($"Portfolio for person with email '{email}' was not found.")
    {
    }
}