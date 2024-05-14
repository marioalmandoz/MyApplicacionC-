using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion.Database;

namespace MyApplicacion.Views;

public partial class RecepcionPage : ContentPage
{

    string referencia;
    public RecepcionPage()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<String>(this, OnDataReceived);
        mostrarPallets();
        
    }


    private async void mostrarPallets()
    {
        List<Pallet> pallet = await App.PalletRepo.MostrarProducciones(referencia);
        palletList.ItemsSource = pallet;
    }
    private void OnDataReceived(object recipient, string message)
    {
        if (message != null)
        {
            Console.WriteLine(message);
            ScanResultLabel.Text = message;
        }
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Views.AlmacenPage");
    }

    private async void ItemButton_Clicked(object sender, EventArgs e)
    {
        // Obtener el objeto de datos asociado a la fila seleccionada
        var item = (Pallet)((Button)sender).BindingContext;

        // Realizar alguna acción con el objeto de datos, por ejemplo:

        bool respuesta = await DisplayAlert("Alerta", $"¿VALIDAR EL PALLET DE FECHA: {item.fecha_hora}? ", "VALIDAR", "INCIDENCIA");
        if (respuesta)
        {
            await App.PalletRepo.tickAlmacen(item.Id);
            string statusMessage = App.PalletRepo.StatusMessage;
            Console.WriteLine(statusMessage);

            await Shell.Current.GoToAsync("///Views.AlmacenPage");
        }
        else
        {
            //WeakReferenceMessenger.Default.Send(item.Id.ToString());

            await Shell.Current.Navigation.PushAsync(new IncidenciasPage(item.Id));
            //await Shell.Current.GoToAsync("///Views.IncidenciasPage");
        }
    }
}