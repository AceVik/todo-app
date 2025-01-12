using TodoApp.Services;
using TodoApp.ViewModels;

namespace TodoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Services (DI) registrieren
            builder.Services.AddSingleton<ITodoApiService, TodoApiService>();

            // ViewModels registrieren
            builder.Services.AddSingleton<MainViewModel>();

            // Views registrieren
            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}
