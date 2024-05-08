
using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion;
using System.Text.RegularExpressions;

namespace ProduccionAlmacen.Views;

public partial class PedidoCajasPage : ContentPage
{
    string referencia;
	public PedidoCajasPage()
	{
		InitializeComponent();
        WeakReferenceMessenger.Default.Register<String>(this, getReferencia);
    }
    private void getReferencia(object recipient, string message)
    {
        if(message != null)
        {
            referencia = message;
        }
    }
    private async void Validar_Clicked(object sender, EventArgs e)
    {
        // Aquí puedes acceder al valor introducido en el Entry
        try
        {
            string numeroPiezas = numeroPiezasEntry.Text;

            numeroPiezasEntry.Text = "";
            Regex regex = new Regex(@"\d");

            if (regex.IsMatch(numeroPiezas))
            {
                // Aquí puedes realizar la validación y cualquier otra lógica necesaria
                // Mostrar una alerta para confirmar si el usuario está seguro
                bool respuesta = await DisplayAlert("Confirmación", $"¿Estás seguro de querer retirar {numeroPiezas} piezas en total?", "Sí", "No");

                // Verificar la respuesta del usuario
                if (respuesta)
                {
                    // TODO: Aquí habrá un método que insertará los datos en la base de datos
                    //App.PalletRepo.AddNewPallet(datosNecesariosParaCrearLaInstancia)


                    //TODO: Esto hay que modificarlo
                    DateTime fecha = DateTime.Now;

                    await App.PalletRepo.RetirarPiezas(numeroPiezas,fecha);


                    //De momento llevamos a la clase ProduccionPage

                    await Shell.Current.GoToAsync("///Views.ReferenciasPedidoPage");

                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Introduce el numero de piezas", "Ok");
        }
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Views.ReferenciasPedidoPage");
    }
}