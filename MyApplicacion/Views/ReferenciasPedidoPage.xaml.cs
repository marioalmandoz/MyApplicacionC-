using MyApplicacion.Database;
using MyApplicacion.Services;

namespace MyApplicacion.Views;

public partial class ReferenciasPedidoPage : ContentPage
{
	public ReferenciasPedidoPage()
	{
		InitializeComponent();
	}

    public async void MostrarPallets(object sender, EventArgs e)
    {
        //TODO: Como puedo recoger el numero de referencia que quiero
        List<Pallet> pallet = await App.PalletRepo.MostrarReferencias("555");
        palletList.ItemsSource = pallet;
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        await Shell.Current.GoToAsync("///PedidoPage");
        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }
}