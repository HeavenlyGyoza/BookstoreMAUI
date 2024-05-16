using BookstoreApp.MVVM.Views.ApplicationPages;
using BookstoreApp.MVVM.Views.LoginPages;
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

            builder.Services.AddSingleton<HttpClient>();

            Routing.RegisterRoute("RegistrationPage", typeof(RegistrationPage));
            Routing.RegisterRoute("ExplorePage", typeof(ExplorePage));

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
