using System.Net.Http.Json;
using TodoApp.Models;
using TodoApp;

namespace TodoApp.Services
{
    public class TodoApiService : ITodoApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://10.0.2.2:5080/";

        public TodoApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<List<ToDoItem>> GetToDoItemsAsync(ItemStatusFilter statusFilter = ItemStatusFilter.All)
        {
            var response = await _httpClient.GetAsync($"/api/ToDoItems?filter={(int)statusFilter}");
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<List<ToDoItem>>();
            return items ?? new List<ToDoItem>();
        }

        public async Task<ToDoItem?> CreateToDoItemAsync(string title, bool isCompleted)
        {
            var body = new
            {
                title,
                isCompleted
            };

            var response = await _httpClient.PostAsJsonAsync("/api/ToDoItems", body);
            if (!response.IsSuccessStatusCode)
                return null;

            var createdItem = await response.Content.ReadFromJsonAsync<ToDoItem>();
            return createdItem;
        }

        public async Task UpdateToDoItemStatusAsync(int id, bool isCompleted)
        {
            var response = await _httpClient.PatchAsJsonAsync($"/api/ToDoItems/{id}", new
            {
                isCompleted
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteToDoItemAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/ToDoItems/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
