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
            builder.Services.AddTransient<Views.OkPage>();

            string dbPath = FileAccessHelper.GetLocalFilePath("app.db3");

            // Crear una instancia de PalletRepository con la ruta de la base de datos
            PalletRepository palletRepository = new PalletRepository(dbPath);

            // Registrar la instancia de PalletRepository en el contenedor de servicios
            builder.Services.AddSingleton<PalletRepository>(palletRepository);


            return builder.Build();
        }
    }
}
