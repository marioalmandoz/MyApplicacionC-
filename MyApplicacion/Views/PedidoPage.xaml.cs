using CommunityToolkit.Maui.Views;
using Plugin.Maui.Audio;
using ProduccionAlmacen.Views;

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
        //if (App.dataAccess.ComprobarReferencia(referencia))
        if (App.dataAccess.ComprobarBaan(referencia))
        {
            App.CurrentPage = "ReferenciasPedidoPage";
            await Shell.Current.Navigation.PushAsync(new ReferenciasPedidoPage(referencia));
           // await Shell.Current.GoToAsync("///Views.ReferenciasPedidoPage");

        }
        else
        {
            AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("error_sound.wav")).Play();
            var popup = new PopUpPage("Error", "INTRODUZCA UNA REFERENCIA EXISTENTE", 1);
            await this.ShowPopupAsync(popup);
            //await DisplayAlert("ERROR", "INTRODUZCA UNA REFERENCIA EXISTENTE", "Ok");
        }
    }

}