using System.ComponentModel.DataAnnotations;

namespace StockMarket.Database.Model;


public class OrderEntity
{
    public int Id { get; set; }

    public OrderAction Action { get; set; }

    public OrderStatus Status { get; set; }

    public int Volume { get; set; }

    public decimal Price { get; set; }

    public DateTime Expires { get; set; }

    public int IdSecurity { get; set; }

    public SecurityEntity Security { get; set; }

    public List<OrderFillEntity> BuyOrderFills { get; set; } = new List<OrderFillEntity>();

    public List<OrderFillEntity> SellOrderFills { get; set; } = new List<OrderFillEntity>();

}
