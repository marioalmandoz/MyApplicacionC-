using MyApplicacion.ViewModels;

namespace MyApplicacion;
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
        await Shell.Current.GoToAsync("///ProduccionPage");
        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }
    private async void Validar_Clicked(object sender, EventArgs e)
    {
        // Aquí puedes acceder al valor introducido en el Entry
        string numeroCajas = numeroCajasEntry.Text;
        numeroCajasEntry.Text = "";


        // Aquí puedes realizar la validación y cualquier otra lógica necesaria
        // Mostrar una alerta para confirmar si el usuario está seguro
        bool respuesta = await DisplayAlert("Confirmación", $"¿Estás seguro de querer introducir {numeroCajas} cajas?", "Sí", "No");

        // Verificar la respuesta del usuario
        if (respuesta)
        {
            //  aqui habra un metodo que metera los datos a una base de datos
            //De momento llevamos a la clase ProduccionPage

            await Shell.Current.GoToAsync("///ProduccionPage");

        }
        else
        {
            //Si el usuario pulsa no
        }
    }


}