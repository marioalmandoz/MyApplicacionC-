using MyApplicacion.Database;
namespace MyApplicacion.Views;

public partial class EditarPalletPage : ContentPage
{
	Pallet item;

	public EditarPalletPage(Pallet pItem)
	{
		InitializeComponent();
		item = pItem;
	}

    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        await Shell.Current.GoToAsync("///Views.DatabaseShow");
        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }
}