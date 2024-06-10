using CommunityToolkit.Maui.Views;
using MyApplicacion.Database;
using ProduccionAlmacen.Views;
namespace MyApplicacion.Views;

public partial class EditarPalletPage : ContentPage
{
	Pallet item;

	public EditarPalletPage(Pallet pItem)
	{
		InitializeComponent();
		item = pItem;
        PasarDatos();
       
	}
    private void PasarDatos()
    {
        refEntry.Text = item.referencia;
        baanEntry.Text = item.baan;
        piezasEntry.Text = item.nPiezas.ToString();
        cajasEntry.Text = item.nCajas;
        ubicaEntry.Text = item.ubicacion;
        producEntry.IsChecked = item.produccion;
        almEntry.IsChecked = item.almacen;
        recEntry.IsChecked = item.rec;
        inciEntry.Text = item.incidencia;
    }

    private async void Go_Back(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
        
    }
    private async void editBtn_Clicked(object sender, EventArgs e)
    {
        item.referencia = refEntry.Text ?? string.Empty;
        item.baan = baanEntry.Text ?? string.Empty;
        item.nPiezas = int.Parse(piezasEntry.Text);
        item.nCajas = cajasEntry.Text ?? string.Empty;
        item.ubicacion = (ubicaEntry.Text ?? string.Empty).ToUpper();
        item.produccion = producEntry.IsChecked;
        item.almacen = almEntry.IsChecked;
        item.rec = recEntry.IsChecked;
        item.incidencia = inciEntry.Text ?? string.Empty;
        Console.WriteLine(item.referencia, item.baan, item.nCajas);

        int exito = App.dataAccess.EditarPallet(item);
        if(exito > 0) {
            PasarDatos();
            var popup = new PopUpPage("Confirmación", "Se ha editado correctamente", 1);
            await this.ShowPopupAsync(popup);
           // await DisplayAlert("Confirmación", "Se ha editado correctamente", "OK");
            await Shell.Current.GoToAsync("///Views.DatabaseShow");
        }
        else{
            var popup = new PopUpPage("Error", "No se ha editado debido a un error", 1);
            await this.ShowPopupAsync(popup);
           // await DisplayAlert("Error", "No se ha editado debido a un error", "OK");
        }
       
    }
}