using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp;

namespace TodoApp.ViewModels
{

    public class StatusFilterOption
    {
        public ItemStatusFilter Value { get; set; }
        public string DisplayText { get; set; }

        public StatusFilterOption(ItemStatusFilter value, string displayText)
        {
            Value = value;
            DisplayText = displayText;
        }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ToDoItem> ToDoItems { get; set; }
            = new ObservableCollection<ToDoItem>();

        private string _newTitle = string.Empty;
        public string NewTitle
        {
            get => _newTitle;
            set
            {
                if (_newTitle != value)
                {
                    _newTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _newIsCompleted;
        public bool NewIsCompleted
        {
            get => _newIsCompleted;
            set
            {
                if (_newIsCompleted != value)
                {
                    _newIsCompleted = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<StatusFilterOption> _filterOptions 
            = new ObservableCollection<StatusFilterOption>
        {
            new StatusFilterOption(ItemStatusFilter.All,       "Alle"),
            new StatusFilterOption(ItemStatusFilter.Todo,      "ToDo"),
            new StatusFilterOption(ItemStatusFilter.Completed, "Abgeschlossen")
        };

        public ObservableCollection<StatusFilterOption> FilterOptions
        {
            get => _filterOptions;
        }

        private StatusFilterOption _selectedFilterOption;
        public StatusFilterOption SelectedFilterOption
        {
            get
            {
                return _selectedFilterOption ??= FilterOptions[0];
            }
            set
            {
                if (_selectedFilterOption != value)
                {
                    _selectedFilterOption = value;
                    OnPropertyChanged();
                    StatusFilter = value.Value;
                }
            }
        }
        
        private ItemStatusFilter _statusFilter = ItemStatusFilter.All;
        public ItemStatusFilter StatusFilter
        {
            get => _statusFilter;
            set
            {
                if (_statusFilter != value)
                {
                    _statusFilter = value;
                    OnPropertyChanged();
                    LoadItemsAsync();
                }
            }
        }

        public ICommand RefreshCommand { get; }
        public ICommand AddCommand { get; }

        private readonly ITodoApiService _todoApiService;

        public MainViewModel(ITodoApiService todoApiService)
        {
            _todoApiService = todoApiService;

            RefreshCommand = new Command(async () => await LoadItemsAsync());
            AddCommand = new Command(async () => await AddItemAsync());
        }


        public async Task LoadItemsAsync()
        {
            var items = _todoApiService.GetToDoItemsAsync(StatusFilter);
            ToDoItems.Clear();
            foreach (var item in await items)
            {
                ToDoItems.Add(item);
            }
        }


        private async Task AddItemAsync()
        {
            if (string.IsNullOrWhiteSpace(NewTitle))
                return;

            var created = await _todoApiService.CreateToDoItemAsync(NewTitle, NewIsCompleted);
            if (created != null)
            {
                ToDoItems.Add(created);
            }

            NewTitle = string.Empty;
            NewIsCompleted = false;
        }


        public async Task DeleteItemAsync(ToDoItem item)
        {
            await _todoApiService.DeleteToDoItemAsync(item.Id);
            ToDoItems.Remove(item);
        }


        public async Task ToggleIsCompleted(ToDoItem item)
        {
            await _todoApiService.UpdateToDoItemStatusAsync(item.Id, item.IsCompleted);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
