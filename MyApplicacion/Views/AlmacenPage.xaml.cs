using CommunityToolkit.Mvvm.Messaging;

namespace MyApplicacion.Views;

public partial class AlmacenPage : ContentPage
{
    public AlmacenPage()
    {
        InitializeComponent();
    }

    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones que quieres realizar cuando se hace clic en el bot�n 1
        // Por ejemplo, mostrar un mensaje en la consola
        await Shell.Current.GoToAsync("///MainPage");
        System.Diagnostics.Debug.WriteLine("�Se hizo clic en el bot�n Back");

    }
    private async void Go_Recepcion(object sender, EventArgs e)
    {
        //WeakReferenceMessenger.Default.Send("1");
        // await Shell.Current.Navigation.PushAsync(new RecepcionPage("1"));
        App.CurrentPage = "RecepcionPage";
        await Shell.Current.GoToAsync("///Views.RecepcionPage");
    }
    private async void Go_ubicacion(object sender, EventArgs e)
    {
        //WeakReferenceMessenger.Default.Send("2");
        //await Shell.Current.Navigation.PushAsync(new UbicacionPage("2"));
        App.CurrentPage = "UbicacioPage";
        await Shell.Current.GoToAsync("///Views.UbicacionPage");
    }
    private async void Go_Pedido(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Views.PedidoPage");
        //await Shell.Current.Navigation.PushAsync(new ReferenciasPedidoPage("1"));
    }
    private async void Db_click(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Views.DatabaseAlmacen");
    }

}