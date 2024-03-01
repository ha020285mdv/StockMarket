using System.ComponentModel.DataAnnotations;

namespace StockMarket.Database.Model;

public enum OrderStatus
{
    Unfilled,
    PartyallyFilled,
    Filled
}
