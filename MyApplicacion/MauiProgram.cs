using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MyApplicacion.Abstractions;
using MyApplicacion.Services;
using MyApplicacion.ViewModels;
/* Cambio no fusionado mediante combinación del proyecto 'MyApplicacion (net8.0-android)'
Antes:
using CommunityToolkit.Maui;
using Plugin.Maui.Audio;
Después:
using Plugin.Maui.Audio;
*/



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

            /*// Main bundle
            Bundle mainBundle = new Bundle();
            mainBundle.PutString("PROFILE_NAME", "<Profile Name>");
            mainBundle.PutString("PROFILE_ENABLED", "false");
            mainBundle.PutString("CONFIG_MODE", "CREATE_IF_NOT_EXIST");

            //PlugingConfig Bundle
            Bundle pluginConfigBundle = new Bundle();
            pluginConfigBundle.PutString("PLUGIN_NAME", "BARCODE");
            pluginConfigBundle.PutString("RESET_CONFIG", "true");

            //ParamList Bundle 
            Bundle paramListBundle = new Bundle();
            paramListBundle.PutString("scanner_selection0", "auto");
            paramListBundle.PutString("scanner_input_enabled", "true");
            paramListBundle.PutString("decoder_code128", "true");
            paramListBundle.PutString("decoder_code39", "true");
            paramListBundle.PutString("decoder_ean8", "true");
            paramListBundle.PutString("decoder_ean13", "true");
            paramListBundle.PutString("decoder_qrcode", "true");

            // Add paramlist Bundle to plugingConfigBundle
            pluginConfigBundle.PutBundle("PARAM_LIST", paramListBundle);

            //Add pluging config bundle to Main bundle
            mainBundle.PutBundle("PLUGIN_CONFIG", pluginConfigBundle);

            // Saved intent with Main Bundle as extra
           //ff sendDataWedgeIntent(mainBundle);
            */
            return builder.Build();
        }
    }
}
