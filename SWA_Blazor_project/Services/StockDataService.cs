using System.Net.Http.Json;
using SWA_Blazor_project.Models;

namespace SWA_Blazor_project.Services
{
    public class StockDataService
    {
        private readonly HttpClient _http;

        public StockDataService(HttpClient http)
        {
            _http = http;
        }

        public async Task<StockPrice[]?> GetStockDataAsync()
        {
            var apiUrl = "/api/StockPrice";
            return await _http.GetFromJsonAsync<StockPrice[]>(apiUrl);
        }
    }
}
