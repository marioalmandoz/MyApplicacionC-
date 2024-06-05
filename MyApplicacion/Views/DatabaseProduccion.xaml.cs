using MyApplicacion;
using MyApplicacion.Database;

namespace MyApplicacion.Views;

public partial class DatabaseProduccion : ContentPage
{
    public DatabaseProduccion()
    {
        InitializeComponent();
        MostrarPallets();
    }
    public async void MostrarPallets()
    {
        List<Pallet> pallet = App.dataAccess.MostrarProduccion();

        palletList.ItemsSource = pallet;
        //-------------------------------------------
        //List<Pallet> pallet = await App.PalletRepo.GetAll();
        //palletList.ItemsSource = pallet;
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        await Shell.Current.GoToAsync("///MainPage");
        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }
    private async void Reload(object sender, EventArgs e)
    {
        MostrarPallets();
    }
}
