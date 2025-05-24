using Ghtk.Repository.Abstractions;
using Ghtk.Repository.Abstractions.Entities;
using MongoDB.Driver;

namespace Ghtk.Repository.MongoDb;

public class OrderRepository : IOrderRepository
{
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IMongoCollection<Order> _orderCollection;

    public OrderRepository(MongoClient mongoClient)
    {

        _mongoClient = mongoClient;
        _mongoDatabase = mongoClient.GetDatabase("ghtk");
        _orderCollection = _mongoDatabase.GetCollection<Order>("orders");
    }
    public async Task CreateOrderAsync(Order orderEntity)
    {
        await this._orderCollection.InsertOneAsync(orderEntity);
    }
}