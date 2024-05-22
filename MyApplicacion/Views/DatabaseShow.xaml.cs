using MyApplicacion.Database;
namespace MyApplicacion.Views;

public partial class DatabaseShow : ContentPage
{
	public DatabaseShow()
	{
		InitializeComponent();
        MostrarPallets();
	}

    public async void MostrarPallets()
    {
        List<Pallet> pallet = App.dataAccess.MostrarDatos();
        palletList.ItemsSource = pallet;
        //-------------------------------------------
        //List<Pallet> pallet = await App.PalletRepo.GetAll();
        //palletList.ItemsSource = pallet;
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        await Shell.Current.GoToAsync("///MainPage");
        System.Diagnostics.Debug.WriteLine("�Se hizo clic en el bot�n Back");

    }
    private async void Reload(object sender, EventArgs e)
    {
        MostrarPallets();
    }
    private async void ItemButton_Clicked(object sender, EventArgs e)
    {
        // Obtener el objeto de datos asociado a la fila seleccionada
        
        var item = (Pallet)((Button)sender).BindingContext;

        // Realizar alguna acci�n con el objeto de datos, por ejemplo:
        bool respuesta = await DisplayAlert("Confirmaci�n", $"�QUIERES EDITAR EL PALLET {item.Id}?", "S�", "No");

        // Verificar la respuesta del usuario
        if (respuesta)
        {
            // TODO: Aqu� habr� un m�todo que insertar� los datos en la base de datos
            await Shell.Current.Navigation.PushAsync(new EditarPalletPage(item));
        }
        else
        {

        }
    }
}