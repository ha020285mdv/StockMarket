using System.ComponentModel.DataAnnotations;

namespace StockMarket.Database.Model;

public class SecurityEntity
{

    public int Id { get; set; }

    public string Symbol { get; set; }

    public string Name { get; set; }

    public ICollection<UserPortfolioEntity> Portfolio { get; set; } = new List<UserPortfolioEntity>();

    public ICollection<OrderEntity> OrderBooks { get; set; } = new List<OrderEntity>();

}
