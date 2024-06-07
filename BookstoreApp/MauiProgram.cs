using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreApp.MVVM.Services;
using BookstoreApp.MVVM.Services.Localization;
using BookstoreApp.MVVM.Views.AdminPages;
using BookstoreApp.MVVM.Views.ApplicationPages;
using BookstoreApp.MVVM.Views.LoginPages;
using BookstoreApp.MVVM.Views.NavigationPages;
using BookstoreApp.MVVM.Views.UserPages;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace BookstoreApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Register services
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<SearchService>();
            builder.Services.AddSingleton<LocalizationResourceManager>();
            //Register ViewModels
            builder.Services.AddTransient<BookViewModel>();
            builder.Services.AddTransient<AuthorViewModel>();
            builder.Services.AddSingleton<ClientViewModel>();
            builder.Services.AddSingleton<AddressViewModel>();
            builder.Services.AddTransient<OrderViewModel>();
            builder.Services.AddTransient<UserViewModel>();
            //Register pages
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<AdminStockPage>();
            builder.Services.AddTransient<AddBookPage>();
            builder.Services.AddTransient<EditBookPage>();
            builder.Services.AddTransient<ItemPage>();
            builder.Services.AddTransient<OrderPage>();
            builder.Services.AddTransient<ExplorePage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<UserPage>();
            builder.Services.AddTransient<MyOrderHistoryPage>();
            builder.Services.AddTransient<AccountSettingsPage>();
            //Register Shell routes
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute($"{nameof(LoginPage)}/{nameof(RegistrationPage)}", typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(ExplorePage), typeof(ExplorePage));
            Routing.RegisterRoute(nameof(ItemPage), typeof(ItemPage));
            Routing.RegisterRoute($"{nameof(ItemPage)}/{nameof(OrderPage)}", typeof(OrderPage));
            Routing.RegisterRoute($"{nameof(ManagementPage)}/{nameof(AdminStockPage)}", typeof(AdminStockPage));
            Routing.RegisterRoute($"{nameof(ManagementPage)}/{nameof(AdminStockPage)}/{nameof(AddBookPage)}", typeof(AddBookPage));
            Routing.RegisterRoute($"{nameof(ManagementPage)}/{nameof(AdminStockPage)}/{nameof(EditBookPage)}", typeof(EditBookPage));
            Routing.RegisterRoute($"{nameof(UserPage)}/{nameof(MyOrderHistoryPage)}", typeof(MyOrderHistoryPage));
            Routing.RegisterRoute($"{nameof(UserPage)}/{nameof(AccountSettingsPage)}", typeof(AccountSettingsPage));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
