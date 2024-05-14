using MyApplicacion.Database;
namespace MyApplicacion.Views;

public partial class EditarPalletPage : ContentPage
{
	Pallet item;

	public EditarPalletPage(Pallet pItem)
	{
		InitializeComponent();
		item = pItem;
        refEntry.Text = item.referencia;
        baanEntry.Text = item.baan;
        piezasEntry.Text = item.nPiezas;
        cajasEntry.Text = item.nCajas;
        ubicaEntry.Text = item.ubicacion;
        producEntry.IsChecked = item.produccion;
        almEntry.IsChecked = item.almacen;
        recEntry.IsChecked = item.rec;
        inciEntry.Text = item.incidencia;

	}

    private async void Go_Back(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Views.DatabaseShow");
        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");
    }
    private async void editBtn_Clicked(object sender, EventArgs e)
    {
        item.referencia = refEntry.Text;
        item.baan = baanEntry.Text;
        item.nPiezas = piezasEntry.Text;
        item.nCajas = cajasEntry.Text;
        item.ubicacion = ubicaEntry.Text;
        item.produccion = producEntry.IsChecked;
        item.almacen = almEntry.IsChecked;
        item.rec = recEntry.IsChecked;
        item.incidencia = inciEntry.Text;
        Console.WriteLine(item.referencia, item.baan, item.nCajas);

        await App.PalletRepo.EditarPallet(item);
    }
}