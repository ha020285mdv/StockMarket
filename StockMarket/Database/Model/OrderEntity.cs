using System.ComponentModel.DataAnnotations;

namespace StockMarket.Database.Model;


public class OrderHistoryEntity
{
    public Guid Id { get; set; }

    public string Ticker { get; set; }

    public SecurityEntity Security { get; set; }

    public int IdUser { get; set; }

    public UserEntity User { get; set; }

    public OrderAction Action { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int VolumeRequested { get; set; }

    public decimal Price { get; set; }

    public DateTime ExpirationDate { get; set; }

    public OrderStatus Status { get; set; }

    public ActiveOrder ActiveOrder { get; set; };

}
