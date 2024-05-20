
using MyApplicacion.Database;
using MyApplicacion;
using System.Data;
using static SQLite.SQLite3;


namespace MyApplicacion.Views;

public partial class DatabasePage : ContentPage
{


    public DatabasePage()
    {
        InitializeComponent();
    }
    public async void AddPallet(object sender, EventArgs e)
    {
        App.dataAccess.AgregarPallet(referencia.Text, Ubicacion.Text);
        Console.WriteLine("A�adido");
        //----------------------------------------------------------------------

        //statusMessage.Text = "";
        //await App.PalletRepo.AddNewPallet(referencia.Text, Ubicacion.Text, Cantidad.Text);
        //statusMessage.Text = App.PalletRepo.StatusMessage;
    }

    public async void MostrarPallets(object sender, EventArgs e)
    {
        List<Pallet> lista = App.dataAccess.MostrarDatos();
        palletList.ItemsSource = lista;
        Console.WriteLine("mostrandoDatos");
        // ----------------------------------------------------------------------

        //statusMessage.Text = "";
        //List<Pallet> pallet = await App.PalletRepo.GetAll();
        //palletList.ItemsSource = pallet;
    }
    public async void EliminarPallet(object sender, EventArgs e)
    {
        int result = App.dataAccess.EliminarPallet(int.Parse(referencia.Text));
        if (result > 0)
        {
            // Pallet eliminado correctamente
            await DisplayAlert("�xito", "Pallet eliminado correctamente.", "OK");
        }
        else
        {
            // No se encontr� el pallet para eliminar
            await DisplayAlert("Error", "No se encontr� el pallet para eliminar.", "OK");
        }

        //-------------------------------------------
        statusMessage.Text = "";
        //App.PalletRepo.EliminarPallets();
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        await Shell.Current.GoToAsync("///MainPage");
        System.Diagnostics.Debug.WriteLine("�Se hizo clic en el bot�n Back");

    }
    
}