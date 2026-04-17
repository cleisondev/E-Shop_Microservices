using BuildingBlocks.Pagination;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersResult(PaginatedResut<OrderDto> Orders);
    
