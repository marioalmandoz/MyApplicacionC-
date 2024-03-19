
using Plugin.Maui.Audio;

namespace MyApplicacion;

public partial class PedidoPage : ContentPage
{
	public PedidoPage()
	{
		InitializeComponent();
	}

    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones que quieres realizar cuando se hace clic en el bot�n 1
        // Por ejemplo, mostrar un mensaje en la consola
        await Shell.Current.GoToAsync("///AlmacenPage");
        System.Diagnostics.Debug.WriteLine("�Se hizo clic en el bot�n Back");

    }
    private async void Validar_Clicked(object sender, EventArgs e)
    {
        // Aqu� puedes acceder al valor introducido en el Entry
        string referencia = referenciaEntry.Text;
        referenciaEntry.Text = "";

        // Verificar la respuesta del usuario
        if (referencia=="1")
        {
            //  aqui habra un metodo que metera los datos a una base de datos
            //De momento llevamos a la clase ProduccionPage

            //MostrarPallets();

            await Shell.Current.GoToAsync("///AlmacenPage");

        }
        else
        {
            AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("error_sound.wav")).Play();

            await DisplayAlert("ERROR", "NO HAY PALLETS DE LA REFERENCIA INTRODUCIDA, INTRODUZCA LA REFERENCIA CORRECTA", "Ok");
        }
    }

}