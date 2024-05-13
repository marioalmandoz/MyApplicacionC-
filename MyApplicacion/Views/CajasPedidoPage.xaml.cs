
using CommunityToolkit.Mvvm.Messaging;
using System.Text.RegularExpressions;

namespace MyApplicacion.Views;

public partial class CajasPedidoPage : ContentPage
{
    int id;
	public CajasPedidoPage(int pId)
	{
		InitializeComponent();
        id = pId;
       // WeakReferenceMessenger.Default.Register<String>(this, getId);
    }
    private void getId(object recipient, string message)
    {
        if(message != null)
        {
            int noId =int.Parse(message);
            if(noId > 0)
            {
                id = noId;
            }
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
                    


                    await App.PalletRepo.RetirarPiezas(numeroPiezas,id);
                    Console.WriteLine(App.PalletRepo.StatusMessage);

                    //De momento llevamos a la clase ProduccionPage

                    await Shell.Current.GoToAsync("///Views.PedidoPage");

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
        // Acciones a realizar al pulsar este boton
        string referencia = await App.PalletRepo.getReferencia(id);
        if (referencia != null)
        {
            await Shell.Current.Navigation.PushAsync(new ReferenciasPedidoPage(referencia));
        }
    }
}