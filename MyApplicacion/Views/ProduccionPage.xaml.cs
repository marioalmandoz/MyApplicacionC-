
using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion.Database;
using Plugin.Maui.Audio;
using System.Text.RegularExpressions;

namespace MyApplicacion.Views;

public partial class ProduccionPage : ContentPage
{
    string cantidad;
    string baan;
    string referencia;
    string scaneo;
    public ProduccionPage()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<String>(this, OnDataReceived);
        
    }
    private void OnDataReceived(object recipient, string message)
    {
        if (message != null)
        {
            Console.WriteLine(message);
            scaneo = message;
            ScanResultLabel.Text = message;
            string pattern = @"Q(\d+).+2K([0-9]{5}).+\/\s([0-9]{5})";
            Match match = Regex.Match(scaneo, pattern);
            if (match.Success)
            {
                cantidad = match.Groups[1].Value;
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
        await Shell.Current.Navigation.PushAsync(new OkPage(pallet));
    }

    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones que quieres realizar cuando se hace clic en el botón 1
        // Por ejemplo, mostrar un mensaje en la consola
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

    // Este método es solo un ejemplo de cómo podrías usar HandleScannerData
    private void ScanBtnClicked(object sender, EventArgs e)
    {
        // Supongamos que has recibido algún tipo de datos del escáner y quieres procesarlos
        WeakReferenceMessenger.Default.Send("78654");

    }



}