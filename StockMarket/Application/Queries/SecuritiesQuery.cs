using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using StockMarket.Database.Model;

namespace StockMarket.Application.Queries;

public class SecuritiesQuery : IRequest<List<SecurityEntity>>
{
    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    [DefaultValue(10)]
    [Range(1, 100)]
    public int PageSize { get; set; } = 10;
}