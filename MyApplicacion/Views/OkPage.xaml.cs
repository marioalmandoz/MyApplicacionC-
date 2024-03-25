using MyApplicacion.ViewModels;
using System.Text.RegularExpressions;

namespace MyApplicacion.Views;
public partial class OkPage : ContentPage
{
	public OkPage(SecondViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}

    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        await Shell.Current.GoToAsync("///Views.ProduccionPage");
        System.Diagnostics.Debug.WriteLine("�Se hizo clic en el bot�n Back");

    }
    private async void Validar_Clicked(object sender, EventArgs e)
    {
        // Aqu� puedes acceder al valor introducido en el Entry
        try
        {
            string numeroCajas = numeroCajasEntry.Text;

            numeroCajasEntry.Text = "";
            Regex regex = new Regex(@"\d");

            if (regex.IsMatch(numeroCajas))
            {
                // Aqu� puedes realizar la validaci�n y cualquier otra l�gica necesaria
                // Mostrar una alerta para confirmar si el usuario est� seguro
                bool respuesta = await DisplayAlert("Confirmaci�n", $"�Est�s seguro de querer introducir {numeroCajas} cajas?", "S�", "No");

                // Verificar la respuesta del usuario
                if (respuesta)
                {
                    // TODO: Aqu� habr� un m�todo que insertar� los datos en la base de datos
                    //App.PalletRepo.AddNewPallet(datosNecesariosParaCrearLaInstancia)



                    //De momento llevamos a la clase ProduccionPage

                    await Shell.Current.GoToAsync("///Views.ProduccionPage");

                }
            }
        }catch (Exception ex)
        {
            await DisplayAlert("Error", $"Introduce el numero de cajas", "Ok");
        }
    }
}