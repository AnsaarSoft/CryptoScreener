using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using Portfolio.Models.Model;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace Portfolio.Services
{
    public class DashboardServices
    {
        private readonly IConfiguration config;
        public readonly HttpClient http = new HttpClient();
        public string symbols = "[\"BTCUSDT\",\"ETHUSDT\",\"SOLUSDT\",\"XRPUSDT\"]";
        private readonly AppDbContext context;
        public DashboardServices(IConfiguration config, HttpClient http, AppDbContext context)
        {
            this.config = config;
            this.http = http;
            this.context = context;
            this.symbols = config.GetValue<string>("BinanceSymbols") ?? "[\"BTCUSDT\",\"ETHUSDT\",\"SOLUSDT\",\"XRPUSDT\"]";
        }

        public async Task<List<TickerModel>?> GetUpdatedPrices()
        {
            var url = $"https://api.binance.com/api/v3/ticker/price?symbols={symbols}";
            var prices = await http.GetFromJsonAsync<List<TickerModel>>(url);
            if (prices is null)
                return null;
            else
                return prices;
        }

        public async Task<List<OrderModel>?> GetUpdatedOrder()
        {
            var collection = new List<OrderModel>();
            collection = await context.Orders
                .AsNoTracking()
                .Where(a => a.BuyPrice != 1)
                .OrderBy(a => a.Name)
                .ToListAsync();
            //collection.Add(new OrderModel() { Id = 1, Name = "AEVOUSDT", BuyPrice = 0, Quantity = 0 });
            //collection.Add(new OrderModel() { Id = 2, Name = "RSRUSDT", BuyPrice = 0.00483, Quantity = 31000 });
            //collection.Add(new OrderModel() { Id = 3, Name = "SAGAUSDT", BuyPrice = 0.121, Quantity = 1000 });
            //collection.Add(new OrderModel() { Id = 4, Name = "TRUUSDT", BuyPrice = 0.0275, Quantity = 36363 });
            //collection.Add(new OrderModel() { Id = 5, Name = "ETHFIUSDT", BuyPrice = 1.105, Quantity = 100 });
            //collection.Add(new OrderModel() { Id = 6, Name = "SKLUSDT", BuyPrice = 0.0214, Quantity = 4672 });
            //collection.Add(new OrderModel() { Id = 7, Name = "MOVRUSDT", BuyPrice = 4.2, Quantity = 120 });
            //collection.Add(new OrderModel() { Id = 8, Name = "INJUSDT", BuyPrice = 0, Quantity = 0 });
            //collection.Add(new OrderModel() { Id = 9, Name = "AAVEUSDT", BuyPrice = 0, Quantity = 0 });
            //collection.Add(new OrderModel() { Id = 9, Name = "PHBUSDT", BuyPrice = 0, Quantity = 0 });
            //collection.Add(new OrderModel() { Id = 9, Name = "XRPUSDT", BuyPrice = 2.7, Quantity = 370 });
            //collection.Add(new OrderModel() { Id = 9, Name = "FETUSDT", BuyPrice = 0.495, Quantity = 1000 });
            //collection.Add(new OrderModel() { Id = 9, Name = "FETUSDT", BuyPrice = 0.545, Quantity = 1000 });
            //collection.Add(new OrderModel() { Id = 9, Name = "TIAUSDT", BuyPrice = 1.443, Quantity = 200 });

            symbols = "[\"" + string.Join("\",\"", collection.Select(s => s.Symbol).Distinct()) + "\"]";
            var prices = await GetUpdatedPrices();
            if (prices is null)
                return null;
            foreach (var order in collection)
            {
                var price = prices.FirstOrDefault(p => p.Symbol == order.Symbol);
                if (price is null) continue;
                order.CurrentPrice = price.Price;
            }
            await Task.Delay(500); // Simulate async operation
            return collection;
        }
    }
}
