
using Plugin.Maui.Audio;

namespace MyApplicacion.Views;

public partial class ProduccionPage : ContentPage
{
    public ProduccionPage()
    {
        InitializeComponent();
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
}