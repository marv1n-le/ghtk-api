using Ghtk.Repository.Abstractions.Entities;

namespace Ghtk.Repository.Abstractions;

public interface IOrderRepository
{
    Task CreateOrderAsync(Order orderEntity);
}