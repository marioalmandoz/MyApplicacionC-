namespace MyApplicacion;

public partial class RecepcionPage : ContentPage
{
	public RecepcionPage()
	{
		InitializeComponent();
	}
    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones que quieres realizar cuando se hace clic en el botón 1
        // Por ejemplo, mostrar un mensaje en la consola
        await Shell.Current.GoToAsync("///AlmacenPage");

        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }

    //TODO: Aqui habra un metodo donde se realizara la comprobacion
    //de esa recepcion y donde se mostrar una tabla con los datos necesarios
    //y donde se haran modificaciones en la Base de datos

}