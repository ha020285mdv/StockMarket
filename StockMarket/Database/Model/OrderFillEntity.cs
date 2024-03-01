using System.ComponentModel.DataAnnotations;

namespace StockMarket.Database.Model;

public class OrderFillEntity
{
    public int Id { get; set; }

    public int Volume { get; set; }

    public decimal Price { get; set; }

    public bool IsSettled { get; set; }

    public int IdBuyOrder { get; set; }

    public OrderEntity BuyOrder { get; set; }

    public int IdSellOrder { get; set; }

    public OrderEntity SellOrder { get; set; }

}
