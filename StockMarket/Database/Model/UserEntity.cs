using System.ComponentModel.DataAnnotations;

namespace StockMarket.Database.Model;

public class UserEntity
{

    public Guid Id { get; set; }

    public string XAPIKey { get; set; }
    
    public string Name { get; set; }

    public string Email { get; set; }
    
    public decimal Funds { get; set; }

    public ICollection<UserPortfolioEntity> Portfolio { get; set; } = new List<UserPortfolioEntity>();

}
