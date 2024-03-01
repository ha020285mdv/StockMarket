using System.ComponentModel.DataAnnotations;

namespace StockMarket.Database.Model;

public class UserPortfolioEntity
{

    public int Id { get; set; }

    public int Number { get; set; }

    public int IdUser { get; set; }

    public UserEntity User { get; set; }

    public int IdSecurity { get; set; }

    public SecurityEntity Security { get; set; }
}
