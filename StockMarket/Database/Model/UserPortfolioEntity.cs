using System.ComponentModel.DataAnnotations;

namespace StockMarket.Database.Model;

public class UserPortfolioEntity
{

    public Guid Id { get; set; }

    public int Number { get; set; }

    public int IdUser { get; set; }

    public UserEntity User { get; set; }

    public string SecurityTicker { get; set; }

    public SecurityEntity Security { get; set; }
}
