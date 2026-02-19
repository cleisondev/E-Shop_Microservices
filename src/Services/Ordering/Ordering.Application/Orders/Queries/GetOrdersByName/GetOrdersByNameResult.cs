
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameResult(IEnumerable<OrderDto> Orders);
