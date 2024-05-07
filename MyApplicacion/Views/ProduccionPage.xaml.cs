
using CommunityToolkit.Mvvm.Messaging;
using Plugin.Maui.Audio;

namespace MyApplicacion.Views;

public partial class ProduccionPage : ContentPage
{
    
    public ProduccionPage()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<String>(this, OnDataReceived);
        
    }
    private void OnDataReceived(object recipient, string message)
    {
        if (message != null)
        {
            ScanResultLabel.Text = message;
        }
    }

    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones que quieres realizar cuando se hace clic en el bot�n 1
        // Por ejemplo, mostrar un mensaje en la consola
        await Shell.Current.GoToAsync("///MainPage");
        System.Diagnostics.Debug.WriteLine("�Se hizo clic en el bot�n Back");

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

    // Este m�todo es solo un ejemplo de c�mo podr�as usar HandleScannerData
    private void ScanBtnClicked(object sender, EventArgs e)
    {
        // Supongamos que has recibido alg�n tipo de datos del esc�ner y quieres procesarlos
        WeakReferenceMessenger.Default.Send("78654");

    }



}