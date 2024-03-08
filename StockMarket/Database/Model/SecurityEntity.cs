using System.ComponentModel.DataAnnotations;

namespace StockMarket.Database.Model;

public class SecurityEntity
{

    public string Ticker { get; set; }

    public string Name { get; set; }

    public ICollection<UserPortfolioEntity> Portfolio { get; set; } = new List<UserPortfolioEntity>();

    public ICollection<OrderHistoryEntity> OrderBooks { get; set; } = new List<OrderHistoryEntity>();

}
