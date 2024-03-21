namespace MyApplicacion;

public partial class UbicacionPage : ContentPage
{
	public UbicacionPage()
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

    //TODO: Aqui Tiene que haber un boton en el que se tenga que poner una ubicacion por lo que se accedera a la Base de datos


}