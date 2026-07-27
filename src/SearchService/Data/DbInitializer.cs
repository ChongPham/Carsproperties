using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Data;

public class DbInitializer
{
    public static async Task InitializeAsync(WebApplication app)
    {
        await DB.InitAsync("SearchDb", MongoClientSettings
            .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

        await DB.Index<Item>()
            .Key(x => x.Make, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();

        // var count = await DB.CountAsync<Item>();

        // if (count == 0)
        // {
        //     Console.WriteLine("Seeding initial data...");
        //     var itemData = await File.ReadAllTextAsync("Data/auctions.json");

        //     var options = new JsonSerializerOptions
        //     {
        //         PropertyNameCaseInsensitive = true
        //     };

        //     var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);

        //     await DB.SaveAsync(items);
        // }

        // Call direct Auction Service to get the latest data and update the search database use HttpClient
        using var scope = app.Services.CreateAsyncScope();
        var httpClient = scope.ServiceProvider.GetRequiredService<AuctionSvcHttpClient>();

        var items = await httpClient.GetItemForSearchDb();

        Console.WriteLine(items.Count + " returned from the auction service");

        if (items.Count > 0) await DB.SaveAsync(items);
    }
}