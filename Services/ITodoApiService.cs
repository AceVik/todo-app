using TodoApp.Models;
using TodoApp;

namespace TodoApp.Services
{
    public interface ITodoApiService
    {
        Task<List<ToDoItem>> GetToDoItemsAsync(ItemStatusFilter statusFilter = ItemStatusFilter.All);
        Task<ToDoItem?> CreateToDoItemAsync(string title, bool isCompleted);
        Task UpdateToDoItemStatusAsync(int id, bool isCompleted);
        Task DeleteToDoItemAsync(int id);
    }
}
