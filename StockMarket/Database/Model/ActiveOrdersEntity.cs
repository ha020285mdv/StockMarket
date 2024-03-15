using System.ComponentModel.DataAnnotations;

namespace StockMarket.Database.Model;


public class ActiveOrdersEntity
{
    public Guid Id { get; set; }

    public string Ticker { get; set; }

    public SecurityEntity Security { get; set; }

    public Guid UserId { get; set; }

    public UserEntity User { get; set; }

    public OrderAction Action { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int VolumeRemaining { get; set; }

    public decimal Price { get; set; }

    public DateTime ExpirationDate { get; set; }

    public OrderStatus Status { get; set; }

    public OrderHistoryEntity OrderHistory { get; set; }

    public ICollection<OrderFillEntity> BuyOrderFills { get; set; } = new List<OrderFillEntity>();

    public ICollection<OrderFillEntity> SellOrderFills { get; set; } = new List<OrderFillEntity>();

}
