using MyApplicacion.Database;

namespace MyApplicacion.Views;

public partial class RecepcionPage : ContentPage
{

    //TODO aqui no me deja cuando le pongo un parametro a la clase 
    //Pero habria que poner SecondViewModel viewModel
    // y esto dentro del metodo  BindingContext = viewModel;
    public RecepcionPage()
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
        // Acciones que quieres realizar cuando se hace clic en el botón 1
        // Por ejemplo, mostrar un mensaje en la consola
        await Shell.Current.GoToAsync("///Views.AlmacenPage");

        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }

    private async void BtnIncidecias_Clicked(object sender, EventArgs e)
    {
        // Acciones que quieres realizar cuando se hace clic en el botón 1
        // Por ejemplo, mostrar un mensaje en la consola
        await Shell.Current.GoToAsync("///Views.IncidenciasPage");

    }

    public async void MostrarPallets(object sender, EventArgs e)
    {
        //TODO: Como puedo recoger el numero de referencia que quiero
        List<Pallet> pallet = await App.PalletRepo.MostrarReferencias("555");
        palletList.ItemsSource = pallet;
    }

    private async void PalletList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Obtener el elemento seleccionado del CollectionView
        var selectedPallet = e.CurrentSelection.FirstOrDefault() as MyApplicacion.Database.Pallet;


        if (selectedPallet != null)
        {
            // Hacer lo que necesites con el pallet seleccionado, por ejemplo, navegar a otra página
            // Pasar el pallet seleccionado como parámetro a la siguiente página
            await DisplayAlert("Alert", $"Ve a la ubicacion {selectedPallet.ubicacion} y retira material", "VALE");
            //await Shell.Current.GoToAsync("///");

            // Desmarcar la selección para permitir futuras selecciones
            // ((CollectionView)sender).SelectedItem = null;
        }
        await Shell.Current.GoToAsync("///Views.IncidenciasPage");
    }

    private async void BtnConfirmar_Clicked(object sender, EventArgs e)
    {
        bool respuesta = await DisplayAlert("Alerta", $"¿ESTAS SEGURO DE QUE QUIERES VALIDAR EL PALLET DE FECHA 27/02/24-08:14? ", "VALIDAR", "CANCELAR");
        if (respuesta)
        {
            await Shell.Current.GoToAsync("///Views.AlmacenPage");
        }
    }

    //TODO: Aqui habra un metodo donde se realizara la comprobacion
    //de esa recepcion y donde se mostrar una tabla con los datos necesarios
    //y donde se haran modificaciones en la Base de datos

}