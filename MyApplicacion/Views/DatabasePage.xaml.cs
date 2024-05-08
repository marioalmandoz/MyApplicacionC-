using MyApplicacion.Database;


namespace MyApplicacion.Views;

public partial class DatabasePage : ContentPage
{


    public DatabasePage()
    {
        InitializeComponent();
    }

    public async void AddPallet(object sender, EventArgs e)
    {

        statusMessage.Text = "";
        await App.PalletRepo.AddNewPallet(referencia.Text, Ubicacion.Text, Cantidad.Text);
        statusMessage.Text = App.PalletRepo.StatusMessage;
    }

    public async void MostrarPallets(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        List<Pallet> pallet = await App.PalletRepo.GetAll();
        palletList.ItemsSource = pallet;
    }
    public async void EliminarPallet(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        App.PalletRepo.EliminarPallets();
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        await Shell.Current.GoToAsync("///MainPage");
        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }
    
}