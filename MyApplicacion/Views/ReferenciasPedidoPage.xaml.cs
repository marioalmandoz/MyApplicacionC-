using MyApplicacion.Database;

namespace MyApplicacion.Views;

public partial class ReferenciasPedidoPage : ContentPage
{
    public ReferenciasPedidoPage()
    {
        InitializeComponent();
        mostrarPallets();
    }

    private async Task mostrarPallets()
    {
        List<Pallet> pallet = await App.PalletRepo.MostrarReferencias("555");
        palletList.ItemsSource = pallet;
    }

    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        await Shell.Current.GoToAsync("///Views.PedidoPage");
        System.Diagnostics.Debug.WriteLine("�Se hizo clic en el bot�n Back");

    }

    private async void PalletList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Obtener el elemento seleccionado del CollectionView
        var selectedPallet = e.CurrentSelection.FirstOrDefault() as MyApplicacion.Database.Pallet;


        if (selectedPallet != null)
        {
            // Hacer lo que necesites con el pallet seleccionado, por ejemplo, navegar a otra p�gina
            // Pasar el pallet seleccionado como par�metro a la siguiente p�gina
            await DisplayAlert("Alert", $"Ve a la ubicacion {selectedPallet.ubicacion} y retira material", "VALE");
            //await Shell.Current.GoToAsync("///");

            // Desmarcar la selecci�n para permitir futuras selecciones
            // ((CollectionView)sender).SelectedItem = null;
        }
        await Shell.Current.GoToAsync("///Views.IncidenciasPage");
    }
}