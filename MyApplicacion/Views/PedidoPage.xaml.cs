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
        // Acciones que quieres realizar cuando se hace clic en el botón 1
        // Por ejemplo, mostrar un mensaje en la consola
        await Shell.Current.GoToAsync("///Views.AlmacenPage");
        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }
    private async void Validar_Clicked(object sender, EventArgs e)
    {
        // Aquí puedes acceder al valor introducido en el Entry

        string referencia = referenciaEntry.Text;
        referenciaEntry.Text = "";

        //


        // Verificar la respuesta del usuario
        if (await App.PalletRepo.ComprobarReferencia(referencia))
        {
            //  aqui habra un metodo que metera los datos a una base de datos
            //De momento llevamos a la clase ProduccionPage

            //MostrarPallets();

            await Shell.Current.GoToAsync("///Views.ReferenciasPedidoPage");

        }
        else
        {
            AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("error_sound.wav")).Play();

            await DisplayAlert("ERROR", "NO HAY PALLETS DE LA REFERENCIA INTRODUCIDA, INTRODUZCA LA REFERENCIA CORRECTA", "Ok");
        }
    }

}