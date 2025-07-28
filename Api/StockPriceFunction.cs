using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Api;

public class StockPriceFunction
{
    private readonly ILogger<StockPriceFunction> _logger;

    public StockPriceFunction(ILogger<StockPriceFunction> logger)
    {
        _logger = logger;
    }

    [Function("StockPrice")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Access-Control-Allow-Origin", "*"); // Adjust for production
        response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");

        if (req.Method.ToUpper() == "OPTIONS")
        {
            return response;
        }

        var random = new Random();
        var stockData = new List<StockPrice>();
        for (var i = 0; i < 100; i++)
        {
            stockData.Add(new StockPrice
            {
                Timestamp = DateTime.Now.AddSeconds(i),
                Price = 100 + random.NextDouble() * 10,
                Ticker = "MSFT"
            });
        }

        var jsonString = JsonSerializer.Serialize(stockData);

        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        await response.WriteStringAsync(jsonString);

        return response;
    }
}

public class StockPrice
{
    public DateTime Timestamp { get; set; }
    public double Price { get; set; }
    public string Ticker { get; set; }
}