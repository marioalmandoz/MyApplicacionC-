using Microsoft.Extensions.Logging;
using MyApplicacion.Services;
using MyApplicacion.Abstractions;
using MyApplicacion.ViewModels;
using CommunityToolkit.Maui;
using Plugin.Maui.Audio;


namespace MyApplicacion
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //Esto es para el contenedor de variables generales
            builder.Services.AddSingleton<ITextProvider, TextProviderService>();
            builder.Services.AddTransient<SecondViewModel>();
            builder.Services.AddTransient<OkPage>();

            return builder.Build();
        }
    }
}
