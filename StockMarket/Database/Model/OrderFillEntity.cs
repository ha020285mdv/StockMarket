using System.ComponentModel.DataAnnotations;

namespace StockMarket.Database.Model;

public class OrderFillEntity
{
    public Guid Id { get; set; }

    public Guid IdBuyOrder { get; set; }

    public ActiveOrdersEntity BuyOrder { get; set; }

    public Guid IdSellOrder { get; set; }

    public ActiveOrdersEntity SellOrder { get; set; }
    public int Volume { get; set; }

    public decimal Price { get; set; }

    public bool IsSettled { get; set; }

    public DateTime CreatedDate { get; set; }

  

}
