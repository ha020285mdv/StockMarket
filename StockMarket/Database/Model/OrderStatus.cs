using System.ComponentModel.DataAnnotations;

namespace StockMarket.Database.Model;

public enum OrderStatus
{
    Unfilled = 1,
    PartyallyFilled = 2,
    Filled = 3,
    Cancelled = 4
}
