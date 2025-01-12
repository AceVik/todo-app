using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel ViewModel => BindingContext as MainViewModel;

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.LoadItemsAsync();
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is ToDoItem item)
            {
                await ViewModel.DeleteItemAsync(item);
            }
        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox cb && cb.BindingContext is ToDoItem item)
            {
                await ViewModel.ToggleIsCompleted(item);
            }
        }
    }
}
