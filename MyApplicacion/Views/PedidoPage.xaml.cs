using CommunityToolkit.Mvvm.Messaging;
using Plugin.Maui.Audio;

namespace MyApplicacion.Views;

public partial class PedidoPage : ContentPage
{
    public PedidoPage()
    {
        InitializeComponent();
       
    }

    private async void Go_Back(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Views.AlmacenPage");
    }
    private async void Validar_Clicked(object sender, EventArgs e)
    {
        //Aqui meto el valor entry
        string referencia = referenciaEntry.Text;
        
        referenciaEntry.Text = "";

        // Verificar la respuesta del usuario
        if (App.dataAccess.ComprobarReferencia(referencia))
        {
            App.CurrentPage = "ReferenciasPedidoPage";
            await Shell.Current.Navigation.PushAsync(new ReferenciasPedidoPage(referencia));
           // await Shell.Current.GoToAsync("///Views.ReferenciasPedidoPage");

        }
        else
        {
            AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("error_sound.wav")).Play();
            await DisplayAlert("ERROR", "NO HAY PALLETS DE LA REFERENCIA INTRODUCIDA, INTRODUZCA LA REFERENCIA CORRECTA", "Ok");
        }
    }

}