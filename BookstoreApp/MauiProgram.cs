using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreApp.MVVM.Services;
using BookstoreApp.MVVM.Views.AdminPages;
using BookstoreApp.MVVM.Views.ApplicationPages;
using BookstoreApp.MVVM.Views.LoginPages;
using BookstoreApp.MVVM.Views.NavigationPages;
using Microsoft.Extensions.Logging;

namespace BookstoreApp
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Register services
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<SearchService>();
            //Register ViewModels
            builder.Services.AddTransient<BookViewModel>();
            builder.Services.AddTransient<AuthorViewModel>();
            builder.Services.AddTransient<ClientViewModel>();
            builder.Services.AddTransient<AddressViewModel>();
            builder.Services.AddTransient<OrderViewModel>();
            builder.Services.AddTransient<UserViewModel>();
            //Register Shell routes
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(ExplorePage), typeof(ExplorePage));
            Routing.RegisterRoute($"{nameof(ManagementPage)}/{nameof(AdminStockPage)}", typeof(AdminStockPage));
            Routing.RegisterRoute($"{nameof(ManagementPage)}/{nameof(AdminStockPage)}/{nameof(AddBookPage)}", typeof(AddBookPage));

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
