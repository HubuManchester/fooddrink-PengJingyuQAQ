using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FoodDrinkApp.Models;

namespace FoodDrinkApp.Services
{
    public static class FoodCatalogService
    {
        private static readonly HttpClient HttpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(12)
        };

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private static List<FoodItem> currentItems = new List<FoodItem>();

        public static async Task<IReadOnlyList<FoodItem>> SearchAsync(string query)
        {
            var items = await GetAllAsync();

            if (string.IsNullOrWhiteSpace(query))
            {
                return items.OrderBy(item => item.Name).ToList();
            }

            var normalised = query.Trim();
            return items
                .Where(item =>
                    (item.Name != null && item.Name.Contains(normalised, StringComparison.OrdinalIgnoreCase)) ||
                    (item.Category != null && item.Category.Contains(normalised, StringComparison.OrdinalIgnoreCase)) ||
                    (item.Tags != null && item.Tags.Contains(normalised, StringComparison.OrdinalIgnoreCase)))
                .OrderBy(item => item.Name)
                .ToList();
        }

        public static async Task<FoodItem> AddAsync(FoodItem item)
        {
            try
            {
                var response = await HttpClient.PostAsJsonAsync(MockApiConfig.EndpointUrl, item, JsonOptions);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<FoodItem>(JsonOptions);
                    if (created != null)
                    {
                        currentItems.Add(created);
                        return created;
                    }
                }
            }
            catch { }

            return item;
        }

        public static async Task<IReadOnlyList<FoodItem>> GetAllAsync()
        {
            try
            {
                var items = await HttpClient.GetFromJsonAsync<List<FoodItem>>(MockApiConfig.EndpointUrl, JsonOptions);
                if (items != null)
                {
                    currentItems = items;
                }
            }
            catch { }

            return currentItems;
        }
    }
}