namespace MyApplicacion;

public partial class AlmacenPage : ContentPage
{
	public AlmacenPage()
	{
		InitializeComponent();
	}

    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones que quieres realizar cuando se hace clic en el botón 1
        // Por ejemplo, mostrar un mensaje en la consola
        await Shell.Current.GoToAsync("///MainPage");
        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }
    private async void Go_Recepcion(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///RecepcionPage");
    }
    private async void Go_ubicacion(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///UbicacionPage");
    }
    private async void Go_Pedido(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PedidoPage");
    }
    private async void Db_click(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Views.DatabasePage");
    }

}