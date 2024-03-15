using System.ComponentModel.DataAnnotations;

namespace StockMarket.Database.Model;

public class UserPortfolioEntity
{
    public int Number { get; set; }

    public Guid UserId { get; set; }

    public UserEntity User { get; set; }

    public string Ticker { get; set; }

    public SecurityEntity Security { get; set; }
}
