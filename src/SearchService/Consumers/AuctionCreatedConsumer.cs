using MassTransit;
using Contracts;
using AutoMapper;
using SearchService.Models;
using MongoDB.Entities;

namespace SearchService.Consumers;

public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
{
    private readonly IMapper _mapper;

    public AuctionCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }    

    public async Task Consume(ConsumeContext<AuctionCreated> context)
    {
        Console.WriteLine(" --> Consuming auction created: " + context.Message.Id);

        var item = _mapper.Map<Item>(context.Message);

        if (item.Model == "Foo1") throw new ArgumentException("Cannot sell cars with name of Foo1");

        await item.SaveAsync();
    }
}