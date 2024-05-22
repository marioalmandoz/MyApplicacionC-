
using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion.Database;
using Plugin.Maui.Audio;
using System.Text.RegularExpressions;

namespace MyApplicacion.Views;

public partial class ProduccionPage : ContentPage
{
    int cantidad;
    string baan;
    string referencia;
    string scaneo;
    public ProduccionPage()
    {
        InitializeComponent();
        
        App.CurrentPage = "ProduccionPage";
        WeakReferenceMessenger.Default.Register<String>(this, OnDataReceived);
        
    }
    private void OnDataReceived(object recipient, string message)
    {
        var parts = message.Split('|');
        if (parts.Length > 1 && parts[0] == "ProduccionPage")
        {
            Console.WriteLine(message);
            scaneo = parts[1];
            ScanResultLabel.Text = message;
            string pattern = @"Q(\d+).+2K([0-9]{5}).+\/\s([0-9]{5})";
            string pattern1 = @"Q(\d+).+2K([0-9]{5})\/([0-9]{5})";
            Match match = Regex.Match(scaneo, pattern);
            if (!match.Success)
            {
                match = Regex.Match(scaneo, pattern1);
            }
            if (match.Success)
            {
                cantidad = int.Parse(match.Groups[1].Value);
                baan = match.Groups[2].Value;
                referencia = match.Groups[3].Value;

                Console.WriteLine("Datos: " + cantidad+" "+baan+" "+referencia);
                ScanResultLabel.Text = "Datos: " + cantidad + " " + baan + " " + referencia;
                registrarPallet();
            }
            else
            {
                Console.WriteLine("No se encontraron coincidencias Referencia.");
            }
        }
    }
    private async void registrarPallet()
    {
        Pallet pallet = new Pallet();
        pallet.baan = baan;
        pallet.referencia = referencia;
        pallet.nPiezas = cantidad;
        ScanResultLabel.Text = null;
        await Shell.Current.Navigation.PushAsync(new OkPage(pallet));
        //bool existe = Peticiones.ComprobarDatos(referencia, cantidad);
        //if (existe)
        //{
        //    await DisplayAlert("Confirmación", "La referencia existe y esta bien la cantidad", "OK");
        //    AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("aprobacion_sound.wav")).Play();

        //    await Shell.Current.Navigation.PushAsync(new OkPage(pallet));
        //}
        //else
        //{
        //   // AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("error_sound.wav")).Play();

        //    await DisplayAlert("Error", "La referencia o la cantidad no coinciden con los previsto", "OK");
        //}

    }

    private async void Go_Back(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");
    }

    private async void VerificarExistencia(object sender, EventArgs e)
    {
        if (1 == 1)
        {
            // https://www.youtube.com/watch?v=oIYnEuZ9oew&t=97s Esto es el link del video de donde he sacado la parte del audio
            AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("aprobacion_sound.wav")).Play();

            await Shell.Current.GoToAsync("///Views.OkPage");
        }
        else
        {
            AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("error_sound.wav")).Play();
            await DisplayAlert("Error", "No se ha introducido correctamente la referencia", "OK");
        }

    }
}