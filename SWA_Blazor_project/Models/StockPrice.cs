namespace SWA_Blazor_project.Models
{
    public class StockPrice
    {
        public DateTime Timestamp { get; set; }
        public double Price { get; set; }
        public string Ticker { get; set; } = string.Empty;
    }
}
